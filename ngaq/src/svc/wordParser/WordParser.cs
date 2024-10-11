using System.Collections.Generic;

namespace ngaq.svc.wordParser;


public class Pos{
	public Pos(){

	}
	public i32 line {get; set;} = 0;
	public i32 col {get; set;} = 0;
}

class Status{
	public ParseState state {get; set;} = ParseState.Start;
	public Pos pos {get; set;} = new Pos();
	public Stack<ParseState> stack {get; set;} = new();

	public IList<str> metadata {get; set;} = new List<str>();

}


public class WordParser{
	I_GetNextChar _GetNextChar;
	Status _status {get; set;}= new Status();

	public Pos pos{
		get{
			return _status.pos;
		}
	}

	public ParseState state{
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
			state = ParseState.End;
			return null;
		}
		if(ans == "\n"){
			pos.line++;
			pos.col = 0;
		}
		return ans;
	}

	public Task<i32> Parse(){
		switch(_status.state){
			case ParseState.Start:
			break;
		}
		return Task.FromResult(0);
	}

	public Task<i32> Start(){
		state = ParseState.TopSpace;
		return Task.FromResult(0);
	}

	public async Task<i32> TopSpace(){
		for(;;){
			var c = await GetNextChar();
			if(c == null){return 0;}
			if(isSpace(c)){
				continue;
			}else if(c == "<"){
				state = ParseState.Metadata;
				_status.stack.Push(ParseState.Metadata);
				break;
			}
			//else if(){}
		}
		//return Task.FromResult(0);
		return 0;
	}

	public async Task<i32> Metadata(){
		for(;;){
			var c = await GetNextChar();
			if(c == null){return 0;}
			if(isSpace(c)){
				continue;
			}else if(c == ">"){

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
}