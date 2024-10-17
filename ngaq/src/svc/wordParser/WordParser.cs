using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using tools;

namespace ngaq.svc.wordParser;

public class ParseErr : std.Exception{
	public ParseErr(str msg):base(msg){

	}

	public I_LineCol? lineCol{get;set;}
	public i32? pos{get;set;}
}

public class Pos : I_LineCol{
	public Pos(){

	}
	public i32 line {get; set;} = 0;
	public i32 col {get; set;} = 0;

	public override str ToString(){
		return $"({line},{col})";
	}
}

class Status{
	public WordParseState state {get; set;} = WordParseState.Start;
	public I_LineCol line_col {get; set;} = new Pos();
	public i32 pos {get;set;} = 0;

	public Stack<WordParseState> stack {get; set;} = new();

	public str curChar {get; set;} = "";

	public IList<str> buffer {get; set;} = new List<str>();

	public IList<str> metadataBuf {get; set;} = new List<str>();

	public WordListTxtMetadata? metadata{get; set;}

	public IList<I_DateBlock> dateBlocks {get; set;} = new List<I_DateBlock>();

	public str headOfWordDelimiter{get;set;} = "";

}

public class Tokens{
	public const str s_metadataTag = "<metadata>";
	public const str e_metadataTag = "</metadata>";
}


public class WordParser{
	I_GetNextChar _GetNextChar;
	public WordParser(I_GetNextChar getNextChar){
		_GetNextChar = getNextChar;
	}
	Status _status {get; set;}= new Status();

	public I_LineCol pos{
		get{
			return _status.line_col;
		}
	}

	public WordParseState state{
		get{
			return _status.state;
		}
		set{
			_status.state = value;
		}
	}

	public IList<str> buffer{
		get{
			return _status.buffer;
		}
	}

	public I_DateBlock getCurDateBlock(){
		if(_status.dateBlocks.Count == 0){
			error("No date block");
			return null!;
		}
		return _status.dateBlocks[_status.dateBlocks.Count - 1];
	}

	public I_WordBlock getCurWordBlock(){
		var curDateBlock = getCurDateBlock();
		if(curDateBlock.words.Count == 0){
			error("No word block"); return null!;
		}
		var curWordBlock = curDateBlock.words[curDateBlock.words.Count - 1];
		return curWordBlock;
	}
	

	protected async Task<str?> GetNextNullableChar(){
		var ans = await _GetNextChar.GetNextChar();
		if(ans == null){
			state = WordParseState.End;
			return null;
		}
		if(ans == "\n"){
			pos.line++;
			pos.col = 0;
		}
		_status.curChar = ans;
		_status.pos++;
		return ans;
	}

	public async Task<str> GetNextChar(){
		var c = await GetNextNullableChar();
		if(c == null){
			error("Unexpected EOF");
			return "";
		}
		return c;
	}


	/// <summary>
	/// 會清空buffer
	/// </summary>
	/// <returns></returns>
	public I_StrSegment bufferToStrSegment(){
		var start = _status.pos - _status.buffer.Count;
		var text = string.Join("", _status.buffer);
		_status.buffer.Clear();
		return new StrSegment{
			start = start
			,text = text
		};
	}


	public code parseMetadataBuffer(IList<str> buffer){
		var txt = string.Join("", buffer);
		var obj = WordListTxtMetadata.Parse(txt);
		if(obj == null){
			error("Invalid metadata");return 1;
		}
		_status.metadata = obj;
		if(obj.delimiter == null || obj.delimiter.Length == 0){
			error("Invalid delimiter");return 1;
		}
		_status.headOfWordDelimiter = obj.delimiter[0].ToString();
		return 0;
	}

	public async Task<IList<I_DateBlock>> Parse(){
		IList<I_DateBlock> ans = new List<I_DateBlock>();
		for(;;){
			switch(_status.state){
				case WordParseState.Start:
					await Start(); // -> TopSpace
				break;
				case WordParseState.TopSpace:
					await TopSpace(); // -> Metadata, DateBlock
				break;
				case WordParseState.Metadata:
					await Metadata(); // -> TopSpace
				break;
				case WordParseState.DateBlock:
					var ua = await ReadDateBlock(); // -> TopSpace
					ans.Add(ua);
				break;
				case WordParseState.End:
					return ans;
				//break;
			}
		}
		//return 0;
	}

	public async Task<i32> Start(){
		state = WordParseState.TopSpace;
		return 0;
	}

	public async Task<i32> TopSpace(){
		for(;;){
			var c = await GetNextNullableChar();
			if(c == null){
				state = WordParseState.End;
				return 0;
			}
			if(isSpace(c)){
				continue;
			}else if(c == "<"){
				state = WordParseState.Metadata;
				_status.stack.Push(WordParseState.Metadata);
				break;
			}else if(c == "["){
				state = WordParseState.DateBlock;
				_status.stack.Push(WordParseState.DateBlock);
				break;
			}
		}
		return 0;
	}

	public async Task<I_DateBlock> ReadDateBlock(){
		var ans = new DateBlock();
		for(;;){
			switch(state){
				case WordParseState.DateBlock:
					_status.state = WordParseState.DateBlock_date;
				break;
				case WordParseState.DateBlock_TopSpace:
					await DateBlock_TopSpace(); // -> Prop, WordBlock
				break;
				case WordParseState.Prop:
					//var prop = await Prop_deprecated(); // -> DateBlock_TopSpace
					var prop = await ReadProp();
					ans.props.Add(prop);
					state = WordParseState.DateBlock_TopSpace;
				break;
				case WordParseState.WordBlock:
					var wordBlock = await ReadWordBlock(); // -> TopSpace
					ans.words.Add(wordBlock);
				break;
				case WordParseState.TopSpace:
					return ans;
				//break;
			}
		}
		//return 0;
	}


	/// 第一行不得寫prop; 將被trim
	/// state -> RestOfWordBlock
	public async Task<I_StrSegment> ReadWordBlockFirstLine(){
		var buf = new List<str>();
		var start = _status.pos;
		for(;;){
			var c = await GetNextChar();
			if(c == "\n"){
				//state = WordParseState.RestOfWordBlock;
				var ans = new StrSegment{
					start = start
					,text = string.Join("", buf)
				};
				return ans;
			}
		}
	}

	// public code chkUnexpectedEOF(str? c){
	// 	if(c == null){
	// 		error("Unexpected EOF");
	// 		return 1;
	// 	}
	// 	return 0;
	// }

	// 不含prop之wordBlockBody
	/// @return body之I_StrSegment、非完整。
	public async Task<I_StrSegment> WordBlockBody(){
		for(;;){
			var c = await GetNextChar();
			if(c == "["){
				state = WordParseState.FirstLeftSquareBracketInWordBlockProp;
				return bufferToStrSegment();
			}else if(c == _status.headOfWordDelimiter){
				state = WordParseState.HeadOfWordDelimiter;
				return bufferToStrSegment();
			}
			buffer.Add(c);
		}
		// var buf = new List<str>();
		// var props = new List<I_Prop>();
		// for(;;){
		// 	var c = await GetNextChar();
		// 	var c2 = await GetNextChar();
		// 	if(c == "[" && c2 == "["){
		// 		buf.RemoveAt(buf.Count - 1);
		// 		var up = await ReadProp();
		// 		props.Add(up);
		// 	}
		// 	buf.Add(c);
		// 	buf.Add(c2);
		// 	//TODO body 正文,分隔符
		// }
	}


/// <summary>
/// -> Prop, RestOfWordBlock
/// </summary>
/// <returns></returns>
	public async Task<code> FirstLeftSquareBracketInWordBlockProp(){
		buffer.Add(_status.curChar); // 加上 "["
		for(;;){
			var c = await GetNextChar();
			if(c == "["){
				state=WordParseState.Prop;
				buffer.Clear();
				break;
			}else{
				state=WordParseState.RestOfWordBlock;
				break;
			}
		}
		return 0;
	}

	// public async Task<code> WordBlockProp(){
	// 	var prop = await ReadProp();
	// 	var curWordBlock = getCurWordBlock();
	// 	curWordBlock.props.Add(prop);
	// 	state = WordParseState.RestOfWordBlock;
	// 	return 0;
	// }

	public async Task<code> HeadOfWordDelimiter(){
		buffer.Add(_status.curChar); // 加上 delimiter首字符
		var delimiter = G.nn(_status?.metadata?.delimiter);
		for(var i = 1;i < delimiter.Length;i++){
			var c = await GetNextChar();
			if(c != delimiter[i].ToString()){
				state = WordParseState.RestOfWordBlock; // not delimiter, deem as word body
				return 0;
			}
		}
		state = WordParseState.WordBlockEnd;
		return 0;
	}

	public async Task<I_WordBlock> ReadWordBlock(){
		I_StrSegment? head = null;
		var bodySegs = new List<I_StrSegment>();
		var props = new List<I_Prop>();
		for(;;){
			switch (state){
				case WordParseState.WordBlock:
					var firstLine = await ReadWordBlockFirstLine(); 
					head = firstLine;
					state = WordParseState.RestOfWordBlock;
				break;
				case WordParseState.RestOfWordBlock:
					var bodySeg = await WordBlockBody();
					bodySegs.Add(bodySeg);
					//state->WordParseState.FirstLeftSquareBracketInWordBlockProp;
				break;
				case WordParseState.FirstLeftSquareBracketInWordBlockProp:
					await FirstLeftSquareBracketInWordBlockProp(); // -> Prop, RestOfWordBlock
				break;
				case WordParseState.Prop:
					//await WordBlockProp(); // -> RestOfWordBlock
					var prop = await ReadProp();
					props.Add(prop);
					state = WordParseState.RestOfWordBlock;
				break;
				case WordParseState.HeadOfWordDelimiter:
					// state -> WordBlockEnd, RestOfWordBlock
					await HeadOfWordDelimiter();
				break;
				case WordParseState.WordBlockEnd:
					state = WordParseState.TopSpace;
					var ans = new WordBlock{
						head = G.nn(head)
						,body = bodySegs
						,props = props
					};
					return ans;
				//break;
			}
		}
	}


////state -> RestOfWordBlock
	// public async Task<code> WordBlockFirstLine(){
	// 	var firstLine = await ReadWordBlockFirstLine(); 
	// 	var curDateBlock = getCurDateBlock();
	// 	var wordBlock = new WordBlock{
	// 		head = firstLine
	// 	};
	// 	curDateBlock.words.Add(wordBlock);
	// 	state = WordParseState.RestOfWordBlock;
	// 	return 0;
	// }

	

	// public async Task<code> WordBlock(){ //TODO
	// 	for(;;){
	// 		switch (state){
	// 			case WordParseState.WordBlock:
	// 				await WordBlockFirstLine(); // -> RestOfWordBlock
	// 			break;
	// 			case WordParseState.RestOfWordBlock:
	// 				await ReadWordBlock(); // -> TopSpace
	// 			break;
	// 		}
	// 	}

	// 	// var tempState = 0; //0:正文; 1:已讀第一個大括號
	// 	// var delimiterBuffer = new List<str>();
	// 	// for(;;){
	// 	// 	switch(tempState){
	// 	// 		case 0: // 正文
	// 	// 			for(;;){
	// 	// 				var c = await GetNextChar();
	// 	// 				if(c == null){error("Unexpected EOF");return 1;}
	// 	// 				if(c == "}"){ // }}
	// 	// 					tempState = 1;
	// 	// 					break;
	// 	// 				}
	// 	// 				if(c == _status.headOfWordDelimiter){
	// 	// 					state = WordParseState.HeadOfWordDelimiter;
	// 	// 					return 0; //TODO
	// 	// 				}
						
	// 	// 				_status.buffer.Add(c);

	// 	// 			}
	// 	// 		break;
	// 	// 		case 1: // 已讀第一個大括號
	// 	// 			for(;;){
	// 	// 				var c = await GetNextChar();
	// 	// 				if(c == null){error("Unexpected EOF");return 1;}
	// 	// 				if(c == "}"){
	// 	// 					state = WordParseState.TopSpace;
	// 	// 					break;
	// 	// 				}else{
	// 	// 					tempState = 0;
	// 	// 				}
	// 	// 			}
	// 	// 		break;
	// 	// 	}

	// 	// }
		
	// 	return 0;
	// }

	// public async Task<bool> IsRestOfWordDelimiter(){
	// 	var delimiterBuffer = new List<str>();
	// 	delimiterBuffer.Add(_status.curChar);//先加上當前字元 潙delimiter之首字符
	// 	for(;;){
	// 		var c = await GetNextChar();
	// 		if(c == null){error("Unexpected EOF");return false;}
	// 		if(delimiterBuffer.Count == G.nn(_status?.metadata?.delimiter?.Length)){
	// 			var joined = string.Join("", delimiterBuffer);
	// 			if(joined == _status.metadata.delimiter){

	// 			}
	// 		}
	// 	}
	// }

	public async Task<code> DateBlock_TopSpace(){
		for(;;){
			var c = await GetNextNullableChar();
			var c2 = await GetNextNullableChar();
			if(c == null || c2 == null){error("Unexpected EOF");return 1;}
			if(isSpace(c)){
				continue;
			}
			if(c == "[" && c2 == "["){
				_status.state = WordParseState.Prop;
				break;
			}else if(c == "{" && c2 == "{"){
				_status.state = WordParseState.WordBlock;
				break;
			}
		}
		return 0;
	}

	public async Task<I_Prop> ReadProp(){
		I_StrSegment? key = null;
		I_StrSegment? value = null;
		
		for(;;){
			switch(state){
				case WordParseState.Prop:
					_status.state = WordParseState.PropKey;
				break;
				case WordParseState.PropKey:
					key = await PropKey();
				break;
				case WordParseState.PropValue:
					value = await PropValue(); // -> DateBlock_TopSpace
				break;
				case WordParseState.DateBlock_TopSpace: // entry
					_status.state = WordParseState.DateBlock_date;
					var ans = new Prop{
						key = key??throw error("key is null")
						,value = value??throw error("value is null")
					};
					return ans;
				//break;
			}
		}
	}

	public async Task<I_Prop> Prop_deprecated(){
		I_StrSegment? key = null;
		I_StrSegment? value = null;
		for(;;){
			switch(state){
				case WordParseState.Prop:
					_status.state = WordParseState.PropKey;
				break;
				case WordParseState.PropKey:
					key = await PropKey();
				break;
				case WordParseState.PropValue:
					value = await PropValue(); // -> DateBlock_TopSpace
				break;
				case WordParseState.DateBlock_TopSpace: // entry
					_status.state = WordParseState.DateBlock_date;
					var ans = new Prop{
						key = key??throw error("key is null")
						,value = value??throw error("value is null")
					};
					return ans;
				//break;
			}
		}
	}

	//@deprecated
	//TODO 理則謬
	public async Task<I_StrSegment> PropValue(){
		for(;;){
			var c = await GetNextNullableChar();
			var c2 = await GetNextNullableChar();
			if(c == null || c2 == null){error("Unexpected EOF"); return null!;}
			if(c == "]" && c2 == "]"){
				_status.buffer.RemoveAt(_status.buffer.Count-1);
				_status.state = WordParseState.DateBlock_TopSpace;
				var value = bufferToStrSegment();
				return value;
			}
			_status.buffer.Add(c);
			_status.buffer.Add(c2);
		}
	}


	public async Task<I_StrSegment> ReadPropValue(){
		var start = _status.pos;
		var buf = new List<str>();
		for(;;){
			var c = await GetNextChar();
			var c2 = await GetNextChar();
			if(c == "]" && c2 == "]"){
				//c != "]" 但 c2 == "]" 旹 、buf加入c,c2後、末字符是"]"、則今除㞢
				buf.RemoveAt(buf.Count-1);
				var value = new StrSegment{
					start = start
					,text = string.Join("", buf)
				};
				return value;
			}
			buf.Add(c);
			buf.Add(c2);
		}
	}

/// <summary>
/// 不改變狀態機、只往後讀字符
/// </summary>
/// <returns></returns>
	public async Task<I_StrSegment> ReadPropKey(){
		var start = _status.pos;
		var buf = new List<str>();
		for(;;){
			var c = await GetNextChar();
			if(c == "|"){
				var joined = string.Join("", buf);
				var key = new StrSegment{
					start = start
					,text = joined
				};
				return key;
			}
			buf.Add(c);
		}
	}

	/// <summary>
	/// @deprecated
	/// </summary>
	/// <returns></returns>
	public async Task<I_StrSegment> PropKey(){
		for(;;){
			var c = await GetNextNullableChar();
			if(c == null){error("Unexpected EOF"); return null!;}
			if(c == "|"){
				_status.state = WordParseState.PropValue;
				var key = bufferToStrSegment();
				return key;
			}
			_status.buffer.Add(c);
		}
	}

	// public async Task<code> DateBlock_date(){
	// 	for(;;){
	// 		var c = await GetNextNullableChar();
	// 		if(c == null){error("Unexpected EOF");return 1;}
	// 		if(c == "]"){
	// 			var date = bufferToStrSegment();
	// 			var dateBlock = new DateBlock{
	// 				date = date
	// 			};
	// 			_status.dateBlocks.Add(dateBlock);
	// 			_status.state = WordParseState.DateBlock_TopSpace;
	// 			break;
	// 		}
	// 		_status.buffer.Add(c);
	// 	}
	// 	return 0;
	// }

	public std.Exception error(str msg){
		var ex = new ParseErr(msg);
		ex.pos = _status.pos;
		ex.lineCol = _status.line_col;
		return ex;
	}

	public async Task<code> Metadata(){
		_status.buffer.Add(_status.curChar); // <
		var metadataStatus = 0; //0:<metadata>; 1:content; 2:</metadata>
		var bracesStack = new List<str>(); //元數據內json之大括號
		var metadataContent = new List<str>();
		for(;;){
			switch(metadataStatus){
				case 0: //<metadata>
					for(;;){
						var c = await GetNextNullableChar();
						if(c == null){error("Unexpected EOF");return 0;}
						_status.buffer.Add(c);
						if(isSpace(c)){
							continue;
						}else if(c == ">"){
							_status.buffer.Add(c); 
							if(chk_metadataStart()){ // joined buffer is <metadata>
								metadataStatus = 1;
								break;
							}
						}
					}
				break;
				case 1:
					for(;;){
						var c = await GetNextNullableChar();
						if(c == null){error("Unexpected EOF"); return 0;}
						metadataContent.Add(c);
						if(c=="{"){
							bracesStack.Add(c);
						}else if(c=="}"){
							if(bracesStack.Count == 0){
								metadataStatus = 2;
								break;
							}else{
								bracesStack.RemoveAt(bracesStack.Count-1);
							}
						}
					}
				break;
				case 2: //</metadata>
					//除ᵣ末大括號到</metadata>間之空白
					for(;;){
						var c = await GetNextNullableChar();
						if(c == null){error("Unexpected EOF");return 0;}
						if(isSpace(c)){
							continue;
						}else if(c == "<"){
							_status.buffer.Add(c);
							break;
						}else{
							error("Unexpected character");
							return 0;
						}
					}

					for(;;){
						var c = await GetNextNullableChar();
						if(c == null){error("Unexpected EOF");return 0;}
						_status.buffer.Add(c);
						if(c == ">"){
							_status.buffer.Add(c);
							if(isMetadataEnd(_status.buffer)){
								//_status.metadataBuf = metadataContent;
								parseMetadataBuffer(metadataContent);
								_status.stack.Pop();
								_status.state = WordParseState.TopSpace;
								return 0;
								//break;
							}
						}
					}
				//break;
			}
		}
	}

	public bool isSpace(str s){
		if(s == " "){return true;}
		if(s == "\t"){return true;}
		if(s == "\n"){return true;}
		if(s == "\r"){return true;}
		return false;
	}

	public bool chk_metadataStart(){
		var ans = false;
		var buf = _status.buffer;
		if(isMetadataStart(buf)){
			ans = true;
		}
		buf.Clear();
		return ans;
	}

	public static bool isMetadataStart(IList<str> buffer){
		if(buffer.Count == Tokens.s_metadataTag.Length){
			var joined = string.Join("", buffer);
			if(joined == Tokens.s_metadataTag){
				return true;
			}
		}
		return false;
	}

	// public bool chk_metadataEnd(){
	// 	var ans = false;
	// 	var buf = _status.buffer;
	// 	if(isMetadataEnd(buf)){
	// 		ans = true;
	// 	}
	// 	buf.Clear();
	// 	return ans;
	// }

	public bool isMetadataEnd(IList<str> buffer){
		if(buffer.Count == Tokens.e_metadataTag.Length){
			var joined = string.Join("", buffer);
			if(joined == Tokens.e_metadataTag){
				return true;
			}
		}
		return false;
	}

}