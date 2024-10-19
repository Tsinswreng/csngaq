using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using tools;

namespace ngaq.svc.wordParser;

//靜態多態
using word = i32;

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

	[Obsolete]
	public Stack<WordParseState> stack {get; set;} = new();

	public word curChar {get; set;} = default;

	public IList<word> buffer {get; set;} = new List<word>();

	public IList<word> metadataBuf {get; set;} = new List<word>();

	public WordListTxtMetadata? metadata{get; set;}

	public IList<I_DateBlock> dateBlocks {get; set;} = new List<I_DateBlock>();

	public word headOfWordDelimiter{get;set;} = default;

}

public class Tokens{
	public const str s_metadataTag = "<metadata>";
	public const str e_metadataTag = "</metadata>";
}


public class WordParser{
	I_GetNextChar_i32 _getNextChar;
	public WordParser(I_GetNextChar_i32 getNextChar){
		_getNextChar = getNextChar;
	}
	Status _status {get; set;}= new Status();

	public I_LineCol lineCol{
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

	public IList<word> buffer{
		get{
			return _status.buffer;
		}
	}

	public Encoding encoding{get; set;} = Encoding.UTF8;

	// [Obsolete]
	// public I_DateBlock getCurDateBlock(){
	// 	if(_status.dateBlocks.Count == 0){
	// 		error("No date block");
	// 		return null!;
	// 	}
	// 	return _status.dateBlocks[_status.dateBlocks.Count - 1];
	// }

	// public I_WordBlock getCurWordBlock(){
	// 	var curDateBlock = getCurDateBlock();
	// 	if(curDateBlock.words.Count == 0){
	// 		error("No word block"); return null!;
	// 	}
	// 	var curWordBlock = curDateBlock.words[curDateBlock.words.Count - 1];
	// 	return curWordBlock;
	// }
	

	protected async Task<word> GetNextNullableChar(){
		var ans = await _getNextChar.GetNextChar();
		if(ans < 0){
			state = WordParseState.End;
			return ans;
		}
		if( eq(ans , '\n') ){
			lineCol.line++;
			lineCol.col = 0;
		}
		_status.curChar = ans;
		_status.pos++;
		return ans;
	}

	public async Task<word> GetNextChar(){
		var c = await GetNextNullableChar();
		if(c < 0){
			error("Unexpected EOF");
			return c;
		}
		return c;
	}

	//編寫期多態
	public bool eq(str s1, str s2){
		return s1 == s2;
	}

	public bool eq(word s1, str s2){
		if(s2.Length > 1){
			return false;
		}
		return s1 == s2[0];
	}

	public bool eq(word s1, char s2){
		return s1 == s2;
	}


	/// <summary>
	/// 會清空buffer
	/// </summary>
	/// <returns></returns>
	public I_StrSegment bufferToStrSegment(){
		var start = _status.pos - _status.buffer.Count;
		var text = bufToStr(_status.buffer);
		_status.buffer.Clear();
		return new StrSegment{
			start = start
			,text = text
		};
	}


	public code parseMetadataBuffer(IList<word> buffer){
		var txt = bufToStr(buffer);
		var obj = WordListTxtMetadata.Parse(txt);
		if(obj == null){
			error("Invalid metadata");return 1;
		}
		_status.metadata = obj;
		if(obj.delimiter == null || obj.delimiter.Length == 0){
			error("Invalid delimiter");return 1;
		}
		_status.headOfWordDelimiter = obj.delimiter[0];
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
			if(c < 0){
				state = WordParseState.End;
				return 0;
			}
			if(isSpace(c)){
				continue;
			}else if( eq(c , '<') ){
				state = WordParseState.Metadata;
				//_status.stack.Push(WordParseState.Metadata);
				break;
			}else if( eq(c , '[') ){
				state = WordParseState.DateBlock;
				//_status.stack.Push(WordParseState.DateBlock);
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

	public string bufToStr(IList<word> buf){
		var encoding = this.encoding;
		//word 是int的別名、幫我實現這個方法。
		if(buf.Count == 0){
			return "";
		}
		var bytes = new byte[buf.Count];
		for (int i = 0; i < buf.Count; i++){
			// 检查是否超出 byte 的范围 (0-255)
			if (buf[i] < 0 || buf[i] > 255){
				throw new std.ArgumentOutOfRangeException(nameof(buf), $"Word value {buf[i]} is out of range for byte.");
			}
			bytes[i] = (byte)buf[i];
		}
		return encoding.GetString(bytes);
	}


	/// 第一行不得寫prop; 將被trim
	/// state -> RestOfWordBlock
	public async Task<I_StrSegment> ReadWordBlockFirstLine(){
		var buf = new List<word>();
		var start = _status.pos;
		for(;;){
			var c = await GetNextChar();
			if( eq(c , '\n')){
				//state = WordParseState.RestOfWordBlock;
				var ans = new StrSegment{
					start = start
					,text = bufToStr(buf)
				};
				return ans;
			}
		}
	}

	// public code chkUnexpectedEOF(str? c){
	// 	if( isNil(c) ){
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
			if( eq(c , '[') ){
				state = WordParseState.FirstLeftSquareBracketInWordBlockProp;
				return bufferToStrSegment();
			}else if(c == _status.headOfWordDelimiter){
				state = WordParseState.HeadOfWordDelimiter;
				return bufferToStrSegment();
			}
			buffer.Add(c);
		}
	}


/// <summary>
/// -> Prop, RestOfWordBlock
/// </summary>
/// <returns></returns>
	public async Task<code> FirstLeftSquareBracketInWordBlockProp(){
		buffer.Add(_status.curChar); // 加上 "["
		for(;;){
			var c = await GetNextChar();
			if( eq(c , "[") ){
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
			if( !eq(c , delimiter[i]) ){
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

	//讀完日期後 、[2024-10-19T15:51:19.877+08:00] 後面
	public async Task<code> DateBlock_TopSpace(){
		for(;;){
			var c = await GetNextChar();
			var c2 = await GetNextChar();
			//if( isNil(c)  ||  isNil(c) ){error("Unexpected EOF");return 1;}
			if(isSpace(c)){
				continue;
			}
			if( eq(c , '[') && eq(c2 , '[')){
				_status.state = WordParseState.Prop;
				break;
			}else if( eq(c , '{') && eq(c2 , '{')){
				_status.state = WordParseState.WordBlock;
				break;
			}
		}
		return 0;
	}


	public async Task<I_Prop> ReadProp(){
		var key = await ReadPropKey();
		var value = await ReadPropValue();
		var ans = new Prop{
			key = key
			,value = value
		};
		return ans;
	}

	public async Task<I_StrSegment> ReadPropValue(){
		var start = _status.pos;
		var buf = new List<word>();
		for(;;){
			var c = await GetNextChar();
			var c2 = await GetNextChar();
			if( eq(c , ']') && eq(c2 , ']') ){
				//c != "]" 但 c2 == "]" 旹 、buf加入c,c2後、末字符是"]"、則今除㞢
				buf.RemoveAt(buf.Count-1);
				var value = new StrSegment{
					start = start
					,text = bufToStr(buf)
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
		var buf = new List<word>();
		for(;;){
			var c = await GetNextChar();
			if( eq(c , '|') ){
				var joined = bufToStr(buf);
				var key = new StrSegment{
					start = start
					,text = joined
				};
				return key;
			}
			buf.Add(c);
		}
	}

	public bool isNil(word s){
		if(s < 0){
			return true;
		}
		return false;
	}

	public std.Exception error(str msg){
		var ex = new ParseErr(msg);
		ex.pos = _status.pos;
		ex.lineCol = _status.line_col;
		return ex;
	}

	public async Task<code> Metadata(){
		_status.buffer.Add(_status.curChar); // <
		var metadataStatus = 0; //0:<metadata>; 1:content; 2:</metadata>
		var bracesStack = new List<word>(); //元數據內json之大括號
		var metadataContent = new List<word>();
		for(;;){
			switch(metadataStatus){
				case 0: //<metadata>
					for(;;){
						var c = await GetNextChar();
						//if( isNil(c) ){error("Unexpected EOF");return 0;}
						_status.buffer.Add(c);
						if(isSpace(c)){
							continue;
						}else if( eq(c , '>') ){
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
						var c = await GetNextChar();
						//if( isNil(c) ){error("Unexpected EOF"); return 0;}
						metadataContent.Add(c);
						if( eq(c,'{') ){
							bracesStack.Add(c);
						}else if( eq(c, '}') ){
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
						var c = await GetNextChar();
						//if( isNil(c) ){error("Unexpected EOF");return 0;}
						if(isSpace(c)){
							continue;
						}else if( eq(c, '<') ){
							_status.buffer.Add(c);
							break;
						}else{
							error("Unexpected character");
							return 0;
						}
					}

					for(;;){
						var c = await GetNextChar();
						//if( isNil(c) ){error("Unexpected EOF");return 0;}
						_status.buffer.Add(c);
						if( eq(c , '>') ){
							_status.buffer.Add(c);
							if(isMetadataEnd(_status.buffer)){
								//_status.metadataBuf = metadataContent;
								parseMetadataBuffer(metadataContent);
								//_status.stack.Pop();
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

	public bool isSpace(word s){
		if( eq(s , ' ') ){return true;}
		if( eq(s , '\t') ){return true;}
		if( eq(s , '\n') ){return true;}
		if( eq(s , '\r') ){return true;}
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

	public bool isMetadataStart(IList<word> buffer){
		if(buffer.Count == Tokens.s_metadataTag.Length){
			var joined = bufToStr(buffer);
			if( eq(joined , Tokens.s_metadataTag) ){
				return true;
			}
		}
		return false;
	}

	public bool isMetadataEnd(IList<word> buffer){
		if(buffer.Count == Tokens.e_metadataTag.Length){
			var joined = bufToStr(buffer);
			if(eq(joined , Tokens.e_metadataTag)){
				return true;
			}
		}
		return false;
	}

}


/* 
var txtLength = 1000000;
for(var i = 0; i < txtLength, i++){
	string c = await GetNextChar(); //c是只有一個碼點的字符串。從文件讀取。
	handle(c);
}
handle方法中會判斷c並把c加入到List<string>中。
以上代碼會有明顯的GC壓力或性能問題嗎?
 */