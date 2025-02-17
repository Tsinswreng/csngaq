using System.Data.Entity;
using db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using model.consts;
using ngaq.Core.model;
using ngaq.Core.model.wordIF;
using ngaq.Core.Svc.Crud;
using ngaq.model.consts;
using ngaq.Server.Db.Crud.IF;


namespace ngaq.Server.Svc.Crud.WordCrud;


public class WordUpdater:
	IDisposable
	,I_SetTx_DbCtx
	,I_UpdAsy<I_FullWordKv>
{
	public WordUpdater() {

	}

	~WordUpdater(){
		Dispose();
	}
	public void Dispose() {
		dbCtx.Dispose();
	}

	public NgaqDbCtx dbCtx{get;set;} = new();

	public async Task<zero> SetTx(IDbContextTransaction transaction) {
		await dbCtx.Database.UseTransactionAsync(transaction.GetDbTransaction());
		return 0;
	}

	public async Task<zero> UpdAsy(I_FullWordKv word){
		dbCtx.Update(word.textWord);
		dbCtx.UpdateRange(word.propertys);
		dbCtx.UpdateRange(word.learns);
		await dbCtx.SaveChangesAsync();
		return 0;
	}
}
