using System.Collections.Generic;

namespace ngaq.svc.wordParser.metadataParser;


public class Status{
	public WordParseState state {get; set;} = WordParseState.Start;
	public I_TxtPos pos {get; set;} = new Pos();
	public Stack<MetadataParseState> stack {get; set;} = new();

	public str curChar {get; set;} = "";

	public IList<str> buffer {get; set;} = new List<str>();

	public IList<str> metadata {get; set;} = new List<str>();

	public I_TxtPos? basePos{get;set;}

}


public class MetadataParser{
	public I_GetNextChar _getNextChar{get;set;}
	public Status status{get;set;} = new Status();

	public async Task<i32> Parse(){

		switch(status.state){
			case WordParseState.Start:
			break;
		}
	}

	public async Task<i32> Start(){

	}
}