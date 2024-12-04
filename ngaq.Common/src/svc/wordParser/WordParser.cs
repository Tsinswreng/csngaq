using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using tools;

using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Diagnostics;
namespace ngaq.svc.wordParser;

//靜態多態
using word = byte;
using WPS = WordParseState;

public class ParseErr : std.Exception{
	public ParseErr(str msg):base(msg){

	}

	public I_LineCol? lineCol{get;set;}
	public u64? pos{get;set;}
}

public class Pos : I_LineCol{
	public Pos(){

	}
	public u64 line {get; set;} = 0;
	public u64 col {get; set;} = 0;

	public override str ToString(){
		return $"({line},{col})";
	}
}

class Status{
	public WPS state {get; set;} = WPS.Start;
	public I_LineCol line_col {get; set;} = new Pos();
	public u64 pos {get;set;} = 0;

	public Stack<WPS> stack {get; set;} = new();

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
	I_getNextByte _getNextByte;
	public i64 byteSize{get;set;}
	public WordParser(I_getNextByte getNextChar, i64 byteSize){
		_getNextByte = getNextChar;
		this.byteSize = byteSize;
	}
	Status _status {get; set;}= new Status();

	public I_LineCol lineCol{
		get{
			return _status.line_col;
		}
	}

	public WPS state{
		get{
			return _status.state;
		}
		set{
			_status.state = value;
		}
	}

	//讀ʹ果ˇ存
	public IList<word> buffer{
		get{
			return _status.buffer;
		}
	}

	public IList<word> preReadBuffer{get;set;} = new List<word>();
	public i32 pos_preRead{get;set;} = 0;

	public Encoding encoding{get; set;} = Encoding.UTF8;

	public bool unifiedNewLine{get; set;} = true;

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
	
	public bool hasNext(){
		return _getNextByte.hasNext();
	}

	protected word tryGetNextByte(){
		var ans = _getNextByte.getNextByte();
		// word ans;
		// if(pos_preRead < preReadBuffer.Count){
		// 	ans = preReadBuffer[pos_preRead];
		// 	pos_preRead++;
		// }else{
		// 	ans =  _getNextChar.GetNextChar();
		// }

		// if(isNil(ans)){
		// 	return ans;
		// }

		// if(unifiedNewLine){
		// 	if( eq(ans, '\r') ){
		// 		var p =  PreRead();
		// 		if( eq(p, '\n') ){
		// 			ans = '\n';
		// 			preReadBuffer.Clear();
		// 			pos_preRead = 0;
		// 			_status.pos++;
		// 		}
		// 	}
		// }

		//lineCol.col++;
		_status.pos++;
		// if( eq(ans , '\n') ){
		// 	lineCol.line++;
		// 	lineCol.col = 0;
		// }
		_status.curChar = ans;
		
		
		return ans;
	}

	i64 _cnt = 0;
	Stopwatch _sw = new Stopwatch();
	public word getNextByte(){
		if(!hasNext()){
			error("Unexpected EOF");
			return 1;
		}
		var c =  tryGetNextByte();
		return c;
	}



	public word getNextChar(u64 pos){
		if(!hasNext()){
			error($"From {pos} to Unexpected EOF");
		}
		var c =  tryGetNextByte();
		return c;
	}
	

	//編寫期多態
	public bool eq(str s1, str s2){
		return s1 == s2;
	}

	// public bool eq(word s1, str s2){
	// 	if(s2.Length > 1){
	// 		return false;
	// 	}
	// 	return s1 == s2[0];
	// }

	public bool eq(word s1, char s2){
		return s1 == s2;
	}

	public bool eq(word s1, word s2){
		return s1 == s2;
	}


	/// <summary>
	/// 會清空buffer
	/// </summary>
	/// <returns></returns>
	public I_StrSegment bufferToStrSegmentEtClr(){
		var start = _status.pos - (u64)_status.buffer.Count;
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
		_status.headOfWordDelimiter = (byte)obj.delimiter[0]; //TODO 
		return 0;
	}

	public IList<I_DateBlock> parse(){
		IList<I_DateBlock> ans = new List<I_DateBlock>();
		for(var i = 0;;i++){
			switch(_status.state){
				case WPS.Start:
					start(); // -> TopSpace
				break;
				case WPS.TopSpace:
					topSpace(); // -> Metadata, DateBlock, End
				break;
				case WPS.Metadata:
					metadata(); // -> TopSpace
				break;
				case WPS.DateBlock:
					var ua = readDateBlock(); // -> TopSpace
					ans.Add(ua);
				break;
				case WPS.End:
					return ans;
				//break;
			}
		}
		//return 0;
	}

	public i32 start(){
		state = WPS.TopSpace;
		return 0;
	}

	public i32 topSpace(){
		for(;;){
			if(!hasNext()){
				state = WPS.End;
				return 0;
			}
			var c =  tryGetNextByte();
			//G.logNoLn((char)(c??48));
			if(isWhite(c)){
				continue;
			}else if( eq(c , '<') ){
				state = WPS.Metadata;
				//_status.stack.Push(WordParseState.Metadata);
				break;
			}else if( eq(c , '[') ){
				state = WPS.DateBlock;
				//_status.stack.Push(WordParseState.DateBlock);
				break;
			}
		}
		return 0;
	}

	public I_StrSegment readDate(){
		var buf = new List<word>();
		var start = _status.pos;
		for(;;){
			var c =  getNextByte();
			if( eq(c , ']') ){
				//state = WordParseState.TopSpace;
				var ans = new StrSegment{
					start = start
					,text = bufToStr(buf)
				};
				return ans;
			}
			buf.Add(c);
		}
	}

	public I_DateBlock readDateBlock(){
		var ans = new DateBlock();
		for(;;){
			switch(state){
				case WPS.DateBlock: //入口
					//_status.state = WordParseState.DateBlock_date;
					ans.date =  readDate();
					state = WPS.DateBlock_TopSpace;
				break;
				case WPS.DateBlock_TopSpace:
					dateBlock_TopSpace(); // -> Prop, WordBlocks
				break;
				case WPS.Prop:
					var prop = readProp();
					ans.props.Add(prop);
					state = WPS.DateBlock_TopSpace;
				break;
				case WPS.WordBlocks:
					var wordBlocks = readWordBlocks(); // -> TopSpace
					foreach(var w in wordBlocks){ans.words.Add(w);}
				break;
				case WPS.DateBlockEnd:
					state = WPS.TopSpace;
				break;
				case WPS.TopSpace:
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
		//var bytes = new byte[buf.Count];
		// for (int i = 0; i < buf.Count; i++){
		// 	// 检查是否超出 byte 的范围 (0-255)
		// 	// if (buf[i] < 0 || buf[i] > 255){
		// 	// 	throw new std.ArgumentOutOfRangeException(nameof(buf), $"Word value {buf[i]} is out of range for byte.");
		// 	// }
		// 	bytes[i] = (byte)buf[i];
		// }
		//var bytes = buf.ToArray();
		return encoding.GetString(buf.ToArray());
	}

	// public str bufToStr(IList<word> buf){
	// 	var encoding = this.encoding;
	// 	//IList<byte> nonNullBuf = (IList<byte>)buf;
	// 	var nonNull
	// 	var arr = nonNullBuf.ToArray();
	// 	return encoding.GetString(arr);
	// }

	public I_StrSegment readLine(){
		var buf = new List<word>();
		var start = _status.pos;
		for(;;){
			var c = getNextByte();
			if( eq(c , '\n')){
				//state = WordParseState.RestOfWordBlock;
				var ans = new StrSegment{
					start = start
					,text = bufToStr(buf)
				};
				return ans;
			}
			buf.Add(c);
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
	public I_StrSegment wordBlockBody(){
		//buffer.Add(_status.curChar);
		for(;;){
			var c =  getNextByte();
			if( eq(c , '[') ){
				var c2 =  getNextByte();
				if( eq(c2, '[') ){
					state = WPS.Prop;
					return bufferToStrSegmentEtClr();
				}else{
					buffer.Add(c);
					buffer.Add(c2);
				}
			}else if( eq(c , _status.headOfWordDelimiter) ){
				state = WPS.HeadOfWordDelimiter;
				_status.stack.Push(WPS.RestOfWordBlock);
				return bufferToStrSegmentEtClr();
			}else if( eq(c , '}')){
				var c2 =  getNextByte();
				if( eq(c2 , '}') ){
					state = WPS.DateBlockEnd; // -> WordBlock_TopSpace -> DateBlockEnd
					return bufferToStrSegmentEtClr();
				}else{
					buffer.Add(c);
					buffer.Add(c2);
				}
			}else{
				buffer.Add(c);
			}
		}


	}


/// <summary>
/// -> Prop, RestOfWordBlock
/// </summary>
/// <returns></returns>
	// public code FirstLeftSquareBracketInWordBlockProp(){
	// 	buffer.Add(_status.curChar); // 加上 第一個 "["
	// 	for(;;){
	// 		var c =  GetNextChar();
	// 		if( eq(c , '[') ){
	// 			state=WPS.Prop;
	// 			buffer.Clear();
	// 			break;
	// 		}else{
	// 			buffer.Add(c);
	// 			state=WPS.RestOfWordBlock;
	// 			break;
	// 		}
	// 	}
	// 	return 0;
	// }


	public code headOfWordDelimiter(){
		var toReturn = _status.stack.Pop();
		return headOfWordDelimiter(toReturn);
	}

	public code headOfWordDelimiter(WPS stateToReturn){
		buffer.Add(_status.curChar); // 加上 delimiter首字符
		var delimiter = G.nn(_status.metadata?.delimiter);
		for(var i = 1;i < delimiter.Length;i++){
			var c =  getNextByte();
			buffer.Add(c);
			if( !eq(c , delimiter[i]) ){
				//state = WordParseState.RestOfWordBlock; // not delimiter
				state = stateToReturn; // 回復原狀態, WordBlock_TopSpace或RestOfWordBlock或FirstLine
				return 0;
			}
		}
		state = WPS.WordBlockEnd;
		buffer.Clear();
		return 0;
	}

	///read until next non-white character
	public word skipWhite(){
		for(;;){
			var c =  getNextByte();
			if(!isWhite(c)){
				return c;
			}

		}
	}


	public code wordBlock_TopSpace(){
		var start = _status.pos;
		for(;;){
			var c =  getNextChar(start);
			//var c =  GetNextNullableChar();
			//G.log(_status.pos,"______"+(char)c);
			if(isWhite(c)){
				continue;
			}
			if( eq(c, _status.headOfWordDelimiter) ){
				state = WPS.HeadOfWordDelimiter;
				_status.stack.Push(WPS.WordBlock_TopSpace);
				return 0;
			}else if( eq(c, '}')){
				var c2 =  getNextByte();
				if(eq(c2, '}')){// }} end of date block
					state = WPS.DateBlockEnd;
					return 0;
				}
			}
			else{
				state = WPS.WordBlockFirstLine;
				return 0;
			}
		}
	}

	public I_StrSegment parseWordBlockHead(){
		buffer.Add(_status.curChar);
		for(;;){
			var c =  getNextByte();
			if( eq(c, '\n') ){
				state = WPS.RestOfWordBlock;
				return bufferToStrSegmentEtClr();
			}
			buffer.Add(c);
		}
	}

	public IList<I_WordBlock> readWordBlocks(){
		var ans = new List<I_WordBlock>();
		var ua = new WordBlock();
		for(;;){
			//G.log(_status.pos, _status.line_col);
			switch(state){
				case WPS.WordBlocks: // 入口
					state = WPS.WordBlock_TopSpace;
				break;
				case WPS.WordBlock_TopSpace:
					wordBlock_TopSpace(); // -> DateBlockEnd, WordBlockFirstLine, headOfWordDelimiter
				break;
				case WPS.WordBlockFirstLine:
					var head =  parseWordBlockHead(); // -> RestOfWordBlock
					if(head == null || head.text.Length == 0){
						continue;
					}
					ua.head = head;
				break;

				case WPS.RestOfWordBlock:
					//state->, HeadOfWordDelimiter, Prop, DateBlockEnd
					var bodySeg = wordBlockBody();
					ua.body.Add(bodySeg);
				break;

				// case WPS.FirstLeftSquareBracketInWordBlockProp:
				// 	 FirstLeftSquareBracketInWordBlockProp(); // -> Prop, RestOfWordBlock
				// break;

				case WPS.Prop:
					// WordBlockProp(); // -> RestOfWordBlock
					var prop = readProp();
					ua.props.Add(prop);
					state = WPS.RestOfWordBlock;
				break;

				case WPS.HeadOfWordDelimiter:
					// state -> state = _status.stack.Pop(), WordBlockEnd
					headOfWordDelimiter();
				break;

				case WPS.WordBlockEnd:
					if(ua.head != null && ua.head.text.Length > 0){
						ans.Add(ua);
						ua = new WordBlock();
					}
					state = WPS.WordBlock_TopSpace;
				break;

				case WPS.DateBlockEnd: // 出口
					ans.Add(ua);
					return ans;
			}
		}
		
	}


/* 
TODO 空wordBlock、及head中有不完整ʹ分隔符者
 */
	// public I_WordBlock? ReadOneWordBlock(){
	// 	I_StrSegment? head = null;
	// 	var bodySegs = new List<I_StrSegment>();
	// 	var props = new List<I_Prop>();
	// 	for(;;){
	// 		G.log(state.ToString());
	// 		switch (state){
	// 			case WPS.WordBlocks: // 入口
	// 				// var firstLine =  ReadLine(); //TODO
	// 				// head = firstLine;
	// 				// state = WordParseState.RestOfWordBlock;
	// 				state = WPS.WordBlock_TopSpace;
	// 			break;
	// 			case WPS.WordBlock_TopSpace:
	// 				 WordBlock_TopSpace(); // -> HeadOfWordDelimiter, FirstLine
	// 			break;
	// 			case WPS.WordBlockFirstLine:
	// 				head =  ParseWordBlockHead(); // -> RestOfWordBlock
	// 				if(head == null || head.text.Length == 0){
	// 					return null;//TODO 判斷純空白
	// 				}
	// 			break;
	// 			case WPS.RestOfWordBlock:
	// 				var bodySeg =  WordBlockBody();
	// 				bodySegs.Add(bodySeg);
	// 				//state->WordParseState.FirstLeftSquareBracketInWordBlockProp;
	// 			break;
	// 			case WPS.FirstLeftSquareBracketInWordBlockProp:
	// 				 FirstLeftSquareBracketInWordBlockProp(); // -> Prop, RestOfWordBlock
	// 			break;
	// 			case WPS.Prop:
	// 				// WordBlockProp(); // -> RestOfWordBlock
	// 				var prop =  ReadProp();
	// 				props.Add(prop);
	// 				state = WPS.RestOfWordBlock;
	// 			break;
	// 			case WPS.HeadOfWordDelimiter:
	// 				// state -> WordBlockEnd, RestOfWordBlock
	// 				 HeadOfWordDelimiter();
	// 			break;
	// 			case WPS.WordBlockEnd:
	// 				state = WPS.WordBlock_TopSpace;
	// 				if(head == null){
	// 					return null;
	// 				}
	// 				var ans = new WordBlock{
	// 					head = head
	// 					,body = bodySegs
	// 					,props = props
	// 				};
	// 				return ans;
	// 			//break;
	// 		}
	// 	}
	// }

	//讀完日期後 、[2024-10-19T15:51:19.877+08:00] 後面
	public code dateBlock_TopSpace(){
		for(;;){
			var c = getNextByte();
			if(isWhite(c)){
				continue;
			}
			if( eq(c , '[') ){
				var c2 =  getNextByte();
				if(eq(c2 , '[')){
					_status.state = WPS.Prop;
					break;
				}else{
					error("Unexpected character");
					return 1;
				}
			}else if( eq(c , '{') ){
				var c2 = getNextByte();
				if(eq(c2 , '{')){
					_status.state = WPS.WordBlocks;
					break;
				}else{
					error("Unexpected character");
					return 1;
				}
			}
		}
		return 0;
	}


	public I_Prop readProp(){
		var key = readPropKey();
		var value = readPropValue();
		var ans = new Prop{
			key = key
			,value = value
		};
		return ans;
	}

	public I_StrSegment readPropValue(){
		var start = _status.pos;
		var buf = new List<word>();
		for(var i = 0;;i++){
			var c = getNextChar(start);
			//var c2 =  PreRead();
			if( eq(c , ']')){
				var c2 = getNextChar(start);
				if( eq(c2, ']') ){
					var value = new StrSegment{
						start = start
						,text = bufToStr(buf)
					};
					return value;
				}else{
					buf.Add(c);
					buf.Add(c2);
					continue;
				}
			}else{
				buf.Add(c);
			}
			
		}
	}

/// <summary>
/// 不改變狀態機、只往後讀字符
/// </summary>
/// <returns></returns>
	public I_StrSegment readPropKey(){
		var start = _status.pos;
		var buf = new List<word>();
		for(;;){
			var c = getNextByte();
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

	// public bool isNil(word s){
	// 	if(s < 0){
	// 		return true;
	// 	}
	// 	return false;
	// }

	[Obsolete]
	public bool isNil(word s){
		if(s == null){
			return true;
		}
		return false;
	}

	public std.Exception error(str msg){
		var ex = new ParseErr(msg);
		ex.pos = _status.pos;
		ex.lineCol = _status.line_col;
		throw ex;
		//return ex;
	}

	public code metadata(){
		_status.buffer.Add(_status.curChar); // <
		var metadataStatus = 0; //0:<metadata>; 1:content; 2:</metadata>
		var bracesStack = new List<word>(); //元數據內json之大括號
		var metadataContent = new List<word>();
		for(;;){
			switch(metadataStatus){
				case 0: //<metadata>
					for(var j = 0; ;j++){
						var c = getNextByte();
						//if( isNil(c) ){error("Unexpected EOF");return 0;}
						buffer.Add(c);
						if( eq(c , '>') ){
							if(chk_metadataStartEtClr()){ // joined buffer is <metadata>
								metadataStatus = 1;
								break;
							}else{
								error("Unexpected character\n");
							}
						}
					}
				break;
				case 1:
					for(;;){
						var c = getNextByte();
						//if( isNil(c) ){error("Unexpected EOF"); return 0;}
						metadataContent.Add(c);
						if( eq(c,'{') ){
							bracesStack.Add(c);
						}else if( eq(c, '}') ){
							if(bracesStack.Count == 0){
								error("metadata content is not valid json"); //大括號不配對
							}else{
								bracesStack.RemoveAt(bracesStack.Count-1);
								if(bracesStack.Count == 0){
									metadataStatus = 2;
									break;
								}
							}
						}
					}
				break;
				case 2: //</metadata>
					//除ᵣ末大括號到</metadata>間之空白
					for(;;){
						var c = getNextByte();
						//if( isNil(c) ){error("Unexpected EOF");return 0;}
						if(isWhite(c)){
							continue;
						}else if( eq(c, '<') ){
							buffer.Add(c);
							break;
						}else{
							error("Unexpected character");
							return 0;
						}
					}

					for(;;){
						var c = getNextByte();
						//if( isNil(c) ){error("Unexpected EOF");return 0;}
						buffer.Add(c);
						if( eq(c , '>') ){
							if(isMetadataEnd(buffer)){
								buffer.Clear();
								//_status.metadataBuf = metadataContent;
								parseMetadataBuffer(metadataContent);
								//_status.stack.Pop();
								_status.state = WPS.TopSpace;
								return 0;
								//break;
							}
						}
					}
				//break;
			}
		}
	}

	public bool isWhite(str s){
		if(s == " "){return true;}
		if(s == "\t"){return true;}
		if(s == "\n"){return true;}
		if(s == "\r"){return true;}
		return false;
	}

	public bool isWhite(word s){
		if( eq(s , ' ') ){return true;}
		if( eq(s , '\t') ){return true;}
		if( eq(s , '\n') ){return true;}
		if( eq(s , '\r') ){return true;}
		return false;
	}

	public bool chk_metadataStartEtClr(){
		var ans = false;
		var buf = buffer;
		if(isMetadataStart(buf)){
			ans = true;
		}
		buf.Clear();
		return ans;
	}

	public bool isMetadataStart(IList<word> buf){
		if(buf.Count == Tokens.s_metadataTag.Length){
			var joined = bufToStr(buf);
			
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

	//only for debug
	public static IList<str> concatBack(IList<I_DateBlock> dateBlocks){
		var ans = new List<str>();
		for(var i = 0; i < dateBlocks.Count; i++){
			var dateBlock = dateBlocks[i];
			ans.Add("[");
			ans.Add(dateBlock.date.text);
			ans.Add("]\n");
			for(var j = 0; j < dateBlock.props.Count; j++){
				var prop = dateBlock.props[j];
				ans.Add("[[");
				ans.Add(prop.key.text);
				ans.Add("|");
				ans.Add(prop.value.text);
				ans.Add("]]\n");
			}
			ans.Add("{{\n");
			for(var j = 0; j < dateBlock.words.Count; j++){
				var word = dateBlock.words[j];
				ans.Add(word.head?.text??"");
				for(var k = 0; k < word.body.Count; k++){
					var body = word.body[k];
					ans.Add(body.text);
				}
				for(var k = 0; k < word.props.Count; k++){
					var prop = word.props[k];
					ans.Add("[[");
					ans.Add(prop.key.text);
					ans.Add("|");
					ans.Add(prop.value.text);
					ans.Add("]]\n");
				}
				ans.Add("````\n");
			}
		}
		return ans;
	}


}


/* 
var txtLength = 1000000;
for(var i = 0; i < txtLength, i++){
	string c =  GetNextChar(); //c是只有一個碼點的字符串。從文件讀取。
	handle(c);
}
handle方法中會判斷c並把c加入到List<string>中。
以上代碼會有明顯的GC壓力或性能問題嗎?
 */



