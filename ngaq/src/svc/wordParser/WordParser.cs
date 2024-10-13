using System.Collections.Generic;
using tools;

namespace ngaq.svc.wordParser;


public class Pos : I_TxtPos{
	public Pos(){

	}
	public i32 line {get; set;} = 0;
	public i32 col {get; set;} = 0;
}

class Status{
	public WordParseState state {get; set;} = WordParseState.Start;
	public I_TxtPos line_col {get; set;} = new Pos();
	public i32 idx {get;set;} = 0;

	public Stack<WordParseState> stack {get; set;} = new();

	public str curChar {get; set;} = "";

	public IList<str> buffer {get; set;} = new List<str>();

	public IList<str> metadataBuf {get; set;} = new List<str>();

	public WordListTxtMetadata? metadata{get; set;}

	public IList<I_DateBlock> dateBlocks {get; set;} = new List<I_DateBlock>();

}

public class Tokens{
	public const str s_metadataTag = "<metadata>";
	public const str e_metadataTag = "</metadata>";
}


public class WordParser{
	I_GetNextChar _GetNextChar;
	Status _status {get; set;}= new Status();

	public I_TxtPos pos{
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
	

	protected async Task<str?> GetNextChar(){
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
		_status.idx++;
		return ans;
	}

	/// <summary>
	/// 會清空buffer
	/// </summary>
	/// <returns></returns>
	public I_StrSegment bufferToStrSegment(){
		var start = _status.idx - _status.buffer.Count;
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
		return 0;
	}

	public async Task<code> Parse(){
		switch(_status.state){
			case WordParseState.Start:
				await Start();
			break;
			case WordParseState.TopSpace:
				await TopSpace();
			break;
			case WordParseState.Metadata:
				await Metadata();
			break;
			case WordParseState.DateBlock:
				await DateBlock();
			break;
		}
		return 0;
	}

	public async Task<i32> Start(){
		state = WordParseState.TopSpace;
		return 0;
	}

	public async Task<i32> TopSpace(){
		for(;;){
			var c = await GetNextChar();
			if(c == null){return 0;}
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

	public async Task<code> DateBlock(){
		for(;;){
			switch(state){
				case WordParseState.DateBlock:
					_status.state = WordParseState.DateBlock_date;
				break;
				case WordParseState.DateBlock_TopSpace:
					await DateBlock_TopSpace();
				break;
			}
		}
		//return 0;
	}

	public async Task<code> DateBlock_TopSpace(){
		for(;;){
			var c = await GetNextChar();
			var c2 = await GetNextChar();
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

	public async Task<I_Prop> Prop(){
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
					value = await PropValue();
				break;
				case WordParseState.DateBlock_TopSpace:
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

	public async Task<I_StrSegment> PropValue(){
		for(;;){
			var c = await GetNextChar();
			var c2 = await GetNextChar();
			if(c == null || c2 == null){error("Unexpected EOF"); return null!;}
			if(c == "]" && c2 == "]"){
				_status.state = WordParseState.DateBlock_TopSpace;
				var value = bufferToStrSegment();
				return value;
			}
			_status.buffer.Add(c);
		}
	}

	public async Task<I_StrSegment> PropKey(){
		for(;;){
			var c = await GetNextChar();
			if(c == null){error("Unexpected EOF"); return null!;}
			if(c == "="){
				_status.state = WordParseState.PropValue;
				var key = bufferToStrSegment();
				return key;
			}
			_status.buffer.Add(c);
		}
	}

	public async Task<code> DateBlock_date(){
		for(;;){
			var c = await GetNextChar();
			if(c == null){error("Unexpected EOF");return 1;}
			if(c == "]"){
				var date = bufferToStrSegment();
				var dateBlock = new DateBlock{
					date = date
				};
				_status.dateBlocks.Add(dateBlock);
				_status.state = WordParseState.DateBlock_TopSpace;
				break;
			}
			_status.buffer.Add(c);
		}
		return 0;
	}

	public std.Exception error(str msg){
		var ex = new std.Exception(msg);
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
						var c = await GetNextChar();
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
						var c = await GetNextChar();
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
						var c = await GetNextChar();
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
						var c = await GetNextChar();
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

	public bool chk_metadataEnd(){
		var ans = false;
		var buf = _status.buffer;
		if(isMetadataEnd(buf)){
			ans = true;
		}
		buf.Clear();
		return ans;
	}

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