using ngaq.Core.model.wordIF;
namespace ngaq.Core.model;

public struct FullWord: I_FullWordKv{
	public I_TextWordKV textWord{get;set;}
	public IList<I_PropertyKv> propertys{get;set;}
	public IList<I_LearnKv> learns{get;set;}
}

