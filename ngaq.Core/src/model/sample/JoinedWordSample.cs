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
	public I_TextWordKV textWord { get; set; } = (I_TextWordKV)new WordKV(){
		id = 0
		,kStr = "test"
		,ct = 1734252180850
		,ut = 1734252193206
		,bl = BlPrefix.join(
			BlPrefix.TextWord
			,"english"
		)
	};

	public I_PropertyKV property { get; set; } = (I_PropertyKV)new WordKV(){
		id = 0
		,kI64 = 0
		,kDesc = KDesc.fKey.ToString()
		,ct = 1734252180850
		,ut = 1734252193206
		,bl = BlPrefix.join(
			BlPrefix.Property
			,PropertyEnum.mean.ToString()
		)
		,vStr = "測試"
	};

	public I_LearnKV learn {get;set;} = (I_LearnKV)new WordKV(){
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

	public I_JoinedWordKV joinedWord{get;set;}

	public JoinedWordSample(){
		joinedWord = new JoinedWord(){
			textWord = this.textWord
			,propertys = [this.property]
			,learns = [this.learn]
		};
	}
}