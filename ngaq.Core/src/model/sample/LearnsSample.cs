using ngaq.Core.model.consts;
using ngaq.model.consts;

namespace ngaq.Core.model.sample;

public class LearnsSample{
	public static LearnsSample? inst = null;
	public static LearnsSample getInst(){
		if(inst == null){
			inst = new LearnsSample();
		}
		return inst;
	}

	public IList<I_LearnKV> sample{get;set;}

	public LearnsSample(){
		sample = null!;
		_init();
	}

	protected zero _init(){

		sample = new List<I_LearnKV>();

		I_LearnKV l1 = new WordKV();
		l1.id = 419;
		l1.bl = BlPrefix.join(BlPrefix.Learn, "");
		l1.wid_(  TextWordSample.getInst().sample.id  );
		l1.learnResult_(LearnEnum.add.ToString());

		I_LearnKV l2 = new WordKV();
		l2.id = 826;
		l2.bl = BlPrefix.join(BlPrefix.Learn, "");
		l2.wid_(  TextWordSample.getInst().sample.id  );
		l2.learnResult_(LearnEnum.fgt.ToString());

		I_LearnKV l3 = new WordKV();
		l3.id = 428;
		l3.bl = BlPrefix.join(BlPrefix.Learn, "");
		l3.wid_(  TextWordSample.getInst().sample.id  );
		l3.learnResult_(LearnEnum.rmb.ToString());


		I_LearnKV l4 = new WordKV();
		l4.id = 468;
		l4.bl = BlPrefix.join(BlPrefix.Learn, "");
		l4.wid_(  TextWordSample.getInst().sample.id  );
		l4.learnResult_(LearnEnum.add.ToString());
		sample.Add(l1);
		sample.Add(l2);
		sample.Add(l3);
		sample.Add(l4);
		return 0;
	}


}
