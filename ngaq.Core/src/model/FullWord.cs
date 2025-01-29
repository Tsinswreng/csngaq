using ngaq.Core.model.wordIF;
namespace ngaq.Core.model;

public struct FullWord: I_FullWordKv{
	public I_TextWordKV textWord{get;set;}
	public IList<I_PropertyKV> propertys{get;set;}
	public IList<I_LearnKV> learns{get;set;}
}

