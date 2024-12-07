namespace ngaq.Core.model.wordIF;

public interface I_JoinedWordKV{
	public I_TextWordKV textWord{get;set;}
	public IList<I_PropertyKV> propertys{get;set;}
	public IList<I_LearnKV> learns{get;set;}
}