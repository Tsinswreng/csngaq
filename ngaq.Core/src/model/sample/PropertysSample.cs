using ngaq.Core.Model.Consts;

namespace ngaq.Core.Model.Sample;

public class PropertysSample{
	public static PropertysSample? inst = null;
	public static PropertysSample getInst(){
		if(inst == null){
			inst = new PropertysSample();
		}
		return inst;
	}

	public IList<I_PropertyKv> sample{get;set;}

	public PropertysSample(){
		sample = null!;
		_init();
	}

	protected zero _init(){
		sample = new List<I_PropertyKv>();

		I_PropertyKv mean1 = new WordKv();
		mean1.id = 293;
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


		I_PropertyKv mean2 = new WordKv();
		mean2.id = 482;
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



		I_PropertyKv source = new WordKv();
		source.id = 387;

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