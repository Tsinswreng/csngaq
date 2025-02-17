using model.consts;
using ngaq.Core.model.consts;
using ngaq.Core.model.wordIF;
using ngaq.model.consts;
namespace ngaq.Core.model.sample;

public class JoinedWordSample{

	protected static JoinedWordSample? inst;

	public static JoinedWordSample getInst(){
		if(inst == null){
			inst = new JoinedWordSample();
		}
		return inst;
	}
	public I_TextWordKV textWord { get; set; } = TextWordSample.getInst().sample;
	// = (I_TextWordKV)new WordKV(){
	// 	id = 0
	// 	,kStr = "audacious"
	// 	,ct = 1734252180850
	// 	,ut = 1734252193206
	// 	,bl = BlPrefix.join(
	// 		BlPrefix.TextWord
	// 		,"english"
	// 	)
	// };

	public I_PropertyKv property { get; set; } = (I_PropertyKv)new WordKv(){
		id = 0
		,kI64 = 0
		,kDesc = KDesc.fKey.ToString()
		,ct = 1734252180850
		,ut = 1734252193206
		,bl = BlPrefix.join(
			BlPrefix.Property
			,PropertyEnum.mean.ToString()
		)
		,vStr =
"""
美: [ɔˈdeɪʃəs]
英: [ɔːˈdeɪʃəs]
adj.	敢于冒險的；大膽的
网絡	魯莽的；大膽創新的；音樂播放器
"""
	};

	public I_LearnKv learn {get;set;} = (I_LearnKv)new WordKv(){
		id = 0
		,kI64 = 0
		,kDesc = KDesc.fKey.ToString()
		,ct = 1734252180850
		,ut = 1734252193206
		,bl = BlPrefix.join(
			BlPrefix.Learn
			,""
		)
		,vStr = LearnEnum.add.ToString()
	};

	public I_FullWordKv joinedWord{get;set;}

	public JoinedWordSample(){
		joinedWord = new FullWord(){
			textWord = this.textWord
			,propertys = [this.property]
			,learns = [this.learn]
		};
	}
}