using System.Data.Entity;
using db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using model.consts;
using ngaq.Core.model;
using ngaq.Core.model.wordIF;
using ngaq.Core.Svc.Crud;
using ngaq.Core.Svc.word.wordMerger;
using ngaq.model.consts;
using ngaq.Server.Db.Crud.IF;
using ngaq.Server.Svc.Crud.WordCrud;
using ngaq.Server.Svc.Crud.WordCrud.IF;

public class WordMerger:
	IDisposable
	,I_SetTx_DbCtx
{
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

	public I_Rm<I_FullWordKv> wordRm = new WordRm();

	public I_Upd<I_FullWordKv> wordUpd = new WordUpdater();


	/// <summary>
	/// 以1潙主
	/// 合併後刪2
	/// //TODO 接口
	/// </summary>
	/// <param name="id1"></param>
	/// <param name="id2"></param>
	/// <returns></returns>
	public async Task<zero> MergeWordEtRm(
		I_FullWordKv word1
		,I_FullWordKv word2
	){
		// var word1 = await wordSeeker.SeekJoinedWordKVById(id1);
		// var word2 = await wordSeeker.SeekJoinedWordKVById(id2);
		// if(word1 == null || word2 == null){
		// 	return 0;
		// }
		wordMergerTool.mergeWord(word1, word2);
		await wordUpd.Upd(word1);
		await wordRm.Rm(word2);
		return 0;
	}

	public async Task<zero> SetTx(IDbContextTransaction transaction) {
		await dbCtx.Database.UseTransactionAsync(transaction.GetDbTransaction());
		return 0;
	}
}
