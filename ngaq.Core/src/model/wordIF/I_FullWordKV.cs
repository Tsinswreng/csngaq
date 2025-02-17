namespace ngaq.Core.Model.wordIF;

public interface I_FullWordKv{
	public I_TextWordKV textWord{get;set;}
	public IList<I_PropertyKv> propertys{get;set;}
	public IList<I_LearnKv> learns{get;set;}
}