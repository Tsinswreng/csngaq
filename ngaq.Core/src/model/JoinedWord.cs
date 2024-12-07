using ngaq.Core.model.wordIF;
namespace ngaq.Core.model;

public struct JoinedWord: I_JoinedWordKV{
	public I_TextWordKV textWord{get;set;}
	public IList<I_PropertyKV> propertys{get;set;}
	public IList<I_LearnKV> learns{get;set;}
}

