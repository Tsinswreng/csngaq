using System.Data.Entity;
using db;
using model.consts;
using ngaq.Core.model;
using ngaq.Core.model.wordIF;
using ngaq.Core.svc.word.wordMerger;
using ngaq.model.consts;
using ngaq.Server.svc.crud.wordCrud;
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

	public I_mergeWord wordMergerTool{get;set;} = WordMergerTool.getInst();

	public WordRm wordRm = new();


	/// <summary>
	/// 以id1潙主
	/// 合併後不刪id2
	/// //TODO 接口 用事務
	/// </summary>
	/// <param name="id1"></param>
	/// <param name="id2"></param>
	/// <returns></returns>
	public async Task<unit> MergeWordEtRm(i64 id1, i64 id2){
		var word1 = await wordSeeker.SeekJoinedWordKVById(id1);
		var word2 = await wordSeeker.SeekJoinedWordKVById(id2);
		if(word1 == null || word2 == null){
			return 0;
		}

		wordMergerTool.mergeWord(word1, word2);


		return 0;
	}
}
