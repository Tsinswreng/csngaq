using System.Data.Entity;
using db;
using model.consts;
using ngaq.Core.model;
using ngaq.Core.model.wordIF;
using ngaq.model.consts;
using ngaq.Server.svc.crud.wordCrud.IF;

public class WordMerger: IDisposable {
	public WordMerger() {

	}

	~WordMerger(){
		Dispose();
	}
	public void Dispose() {
		dbCtx?.Dispose();
	}

	public NgaqDbCtx dbCtx{get;set;} = new();
	public I_SeekJoinedWordKVById wordSeeker{get;set;}//TODO

	//TODO 遷至Core
	public async Task<unit> mergeWord(I_JoinedWordKV word1, I_JoinedWordKV word2){
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

	/// <summary>
	/// 以id1潙主
	/// 合併後不刪id2
	/// //TODO 接口
	/// </summary>
	/// <param name="id1"></param>
	/// <param name="id2"></param>
	/// <returns></returns>
	public async Task<unit> GetMergeWordNoRm(i64 id1, i64 id2){
		var word1 = await wordSeeker.SeekJoinedWordKVById(id1);
		var word2 = await wordSeeker.SeekJoinedWordKVById(id2);
		if(word1 == null || word2 == null){
			return 0;
		}


		return 0;
	}
}
