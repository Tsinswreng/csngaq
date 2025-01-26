using ngaq.Core.model.consts;

namespace ngaq.Core.model.sample;

public class PropertysSample{
	public static PropertysSample? inst = null;
	public static PropertysSample getInst(){
		if(inst == null){
			inst = new PropertysSample();
		}
		return inst;
	}

	public IList<I_PropertyKV> sample{get;set;}

	public PropertysSample(){
		sample = null!;
		_init();
	}

	protected zero _init(){
		sample = new List<I_PropertyKV>();

		I_PropertyKV mean1 = new WordKV();
		mean1.wid_(  TextWordSample.getInst().sample.id  );
		mean1.setStr(
			PropertyEnum.mean.ToString(),
"""
美: [ɔˈdeɪʃəs]
英: [ɔːˈdeɪʃəs]
adj.	敢于冒險的；大膽的
网絡	魯莽的；大膽創新的；音樂播放器
mean2
"""
		);


		sample.Add(mean1);


		I_PropertyKV mean2 = new WordKV();
		mean2.wid_(  TextWordSample.getInst().sample.id  );

		mean2.setStr(
			PropertyEnum.mean.ToString(),
"""
美: [ɔˈdeɪʃəs]
英: [ɔːˈdeɪʃəs]
adj.	敢于冒險的；大膽的
网絡	魯莽的；大膽創新的；音樂播放器
mean2
"""
		);


		sample.Add(mean2);



		I_PropertyKV source = new WordKV();
		source.wid_(  TextWordSample.getInst().sample.id  );
		source.setStr(
			PropertyEnum.source.ToString()
			, "testSource"
		);
		// source.kStr_(  PropertyEnum.source.ToString()  );
		// source.vStr_(  "testSource"  );
		sample.Add(source);
		return 0;
	}

}