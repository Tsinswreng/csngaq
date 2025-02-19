using ngaq.Core.Model.wordIF;

namespace ngaq.Core.Model.Sample;

public class FullWordSample{

	public static FullWordSample? inst = null;
	public static FullWordSample getInst(){
		if(inst == null){
			inst = new FullWordSample();
		}
		return inst;
	}

	public I_FullWordKv sample{get;set;}

	public FullWordSample(){
		sample = null!;
		_init();
	}

	protected zero _init(){
		sample = new FullWord();
		sample.textWord = TextWordSample.getInst().sample;
		sample.propertys = PropertysSample.getInst().sample;
		sample.learns = LearnsSample.getInst().sample;
		return 0;
	}


}