using System.Collections;
using System.Collections.Generic;
using tools;
namespace ngaq.svc.wordParser;

//using Prop = Dictionary<I_StrSegment, I_StrSegment>;

/* 
type pair = [number, string]
const myPair: pair = [1, "hello"]
 */


public interface I_Prop{
	public I_StrSegment key{get;set;}
	public I_StrSegment value{get;set;}
}

public struct Prop : I_Prop{
	
	public I_StrSegment key{get;set;}
	public I_StrSegment value{get;set;}
}

public interface I_DateBlock{
	public I_StrSegment date{get;set;}
	public IList<WordBlock> words{get;set;}
	
	public IList<I_Prop> props{get;set;}
}

public struct DateBlock:I_DateBlock{
	public I_StrSegment date{get;set;}
	public IList<WordBlock> words{get;set;}
	
	public IList<I_Prop> props{get;set;}
}





public interface I_WordBlock{
	public I_StrSegment head{get;set;}
	//body蜮不連續、可被prop打斷、故用IList洏不用單個I_StrSegment
	public IList<I_StrSegment> body{get;set;} 
	public IList<I_Prop> props{get;set;}
}

public struct WordBlock : I_WordBlock{
	public I_StrSegment head{get;set;}
	public IList<I_StrSegment> body{get;set;}
	public IList<I_Prop> props{get;set;}
}