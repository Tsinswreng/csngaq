using System.Collections.Generic;
using Avalonia.Animation;

namespace ngaq.svc.wordParser;


public class Pos : I_TxtPos{
	public Pos(){

	}
	public i32 line {get; set;} = 0;
	public i32 col {get; set;} = 0;
}

class Status{
	public WordParseState state {get; set;} = WordParseState.Start;
	public I_TxtPos pos {get; set;} = new Pos();
	public Stack<WordParseState> stack {get; set;} = new();

	public str curChar {get; set;} = "";

	public IList<str> buffer {get; set;} = new List<str>();

	public IList<str> metadata {get; set;} = new List<str>();

	

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
			return _status.pos;
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
		return ans;
	}

	public Task<i32> Parse(){
		switch(_status.state){
			case WordParseState.Start:
			break;
		}
		return Task.FromResult(0);
	}

	public Task<i32> Start(){
		state = WordParseState.TopSpace;
		return Task.FromResult(0);
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
			}
			//else if(){}
		}
		//return Task.FromResult(0);
		return 0;
	}

	public i32 error(str msg){
		throw new std.Exception(msg);
	}

	public async Task<i32> Metadata(){
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
								_status.metadata = metadataContent;
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
		if(isMetadataStart(_status.buffer)){
			ans = true;
		}
		_status.metadata.Clear();
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
		if(isMetadataEnd(_status.buffer)){
			ans = true;
		}
		_status.metadata.Clear();
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