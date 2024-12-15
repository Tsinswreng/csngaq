using ngaq.Core.model;
using ngaq.Core.model.wordIF;

namespace ngaq.Core.svc.word.wordMerger;

public class WordMergerTool:
	I_mergeWord
{
	protected static WordMergerTool inst;
	public static WordMergerTool getInst(){
		if(inst == null){
			inst = new WordMergerTool();
		}
		return inst;
	}
	public unit mergeWord(I_JoinedWordKV word1, I_JoinedWordKV word2){
		if(word1.textWord.lang_() != word2.textWord.lang_()){
			throw new ArgumentException("language not match");
		}

		foreach(var e in word2.learns){
			word1.learns.Add(e);
		}
		foreach(var e in word2.propertys){
			word1.propertys.Add(e);
		}

		return 0;
	}
}