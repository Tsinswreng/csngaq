using ngaq.Core.model.consts;

namespace ngaq.Core.model.sample;

public class LearnsSample{
	public static LearnsSample? inst = null;
	public static LearnsSample getInst(){
		if(inst == null){
			inst = new LearnsSample();
		}
		return inst;
	}

	public IList<I_LearnKV> samples{get;set;}

	public LearnsSample(){
		samples = null!;
		_init();
	}

	protected zero _init(){

		samples = new List<I_LearnKV>();

		I_LearnKV l1 = new WordKV();
		l1.wid_(  TextWordSample.getInst().sample.id  );
		l1.learnResult_(LearnEnum.add.ToString());

		I_LearnKV l2 = new WordKV();
		l2.wid_(  TextWordSample.getInst().sample.id  );
		l2.learnResult_(LearnEnum.fgt.ToString());

		I_LearnKV l3 = new WordKV();
		l3.wid_(  TextWordSample.getInst().sample.id  );
		l3.learnResult_(LearnEnum.rmb.ToString());


		I_LearnKV l4 = new WordKV();
		l4.wid_(  TextWordSample.getInst().sample.id  );
		l4.learnResult_(LearnEnum.add.ToString());
		samples.Add(l1);
		samples.Add(l2);
		samples.Add(l3);
		samples.Add(l4);
		return 0;
	}


}