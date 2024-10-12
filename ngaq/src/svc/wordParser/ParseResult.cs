using System.Collections.Generic;
using tools;
namespace ngaq.svc.wordParser;

using Prop = Dictionary<str, I_StrSegment>;


public interface I_DateBlock{
	public I_StrSegment date{get;set;}
	public IList<WordBlock> words{get;set;}
	
	public Prop prop{get;set;}
}

public struct DateBlock:I_DateBlock{
	public I_StrSegment date{get;set;}
	public IList<WordBlock> words{get;set;}
	
	public Prop prop{get;set;}
}





public interface I_WordBlock{
	public I_StrSegment head{get;set;}
	public I_StrSegment body{get;set;}
	public Prop prop{get;set;}
}

public struct WordBlock : I_WordBlock{
	public I_StrSegment head{get;set;}
	public I_StrSegment body{get;set;}
	public Prop prop{get;set;}
}