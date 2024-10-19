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
	public Prop(){}
	public I_StrSegment key{get;set;} // 允許褈複key
	public I_StrSegment value{get;set;}
}

public interface I_DateBlock{
	public I_StrSegment date{get;set;}
	public IList<I_WordBlock> words{get;set;}
	
	public IList<I_Prop> props{get;set;}
}

public struct DateBlock:I_DateBlock{
	//protected DateBlock(){}
	public DateBlock(){}//不寫此則不初始化ᵣ直ᵈ賦值之字段ˇ
	public DateBlock(I_StrSegment date){
		this.date = date;
	}
	public I_StrSegment date{get;set;}
	public IList<I_WordBlock> words{get;set;} = new List<I_WordBlock>();
	
	public IList<I_Prop> props{get;set;} = new List<I_Prop>();
}


public interface I_WordBlock{
	public I_StrSegment head{get;set;}
	//body蜮不連續、可被prop打斷、故用IList洏不用單個I_StrSegment
	public IList<I_StrSegment> body{get;set;} 
	public IList<I_Prop> props{get;set;}
}

public struct WordBlock : I_WordBlock{
	public WordBlock(){}
	public WordBlock(I_StrSegment head){
		this.head = head;
	}
	public I_StrSegment head{get;set;}
	public IList<I_StrSegment> body{get;set;} = new List<I_StrSegment>();
	public IList<I_Prop> props{get;set;} = new List<I_Prop>();
}


// public struct S{
// 	public S(string n){
// 		this.name = n;
// 	}
// 	public string name{get;set;} = "";
// 	public string value{get;set;}

// }

// public class T{
// 	public static void Main(){
// 		var s = new S();//爲甚麼我可以調用無參構造器?
// 	}
// }