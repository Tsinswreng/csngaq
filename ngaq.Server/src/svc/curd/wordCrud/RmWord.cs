using ngaq.Core.model;
using ngaq.Core.svc.crud;
using ngaq.Server.db.crud;
using ngaq.Model.Consts;
using ngaq.Core.model.wordIF;
using tools.IF;
using ngaq.Server.Db.Crud.IF;
using Microsoft.EntityFrameworkCore.Storage;
using ngaq.Core.model.Consts;
using model.consts;
using ngaq.Core.svc;
using db;
using Microsoft.EntityFrameworkCore;

namespace ngaq.Server.svc.crud.wordCrud;


public class WordRm:
	IDisposable
	,I_SetTx_DbCtx
	,I_RmAsy<I_FullWordKv>
{
	public WordRm() {

	}

	~WordRm(){
		Dispose();
	}

	public WordSeeker wordSeeker = new ();

	public NgaqDbCtx dbCtx = new ();

	public void Dispose() {

	}


	public async Task<zero> RmAsy(I_FullWordKv joinedWordKV){
		dbCtx.WordKV.Remove((WordKv)joinedWordKV.textWord);
		dbCtx.WordKV.RemoveRange((IList<WordKv>)joinedWordKV.propertys);
		dbCtx.WordKV.RemoveRange((IList<WordKv>)joinedWordKV.learns);
		await dbCtx.SaveChangesAsync();
		return 0;
	}

	public async Task<zero> SetTx(IDbContextTransaction transaction) {
		await dbCtx.Database.UseTransactionAsync(transaction.GetDbTransaction());
		return 0;
	}

	// public async Task<code> RmById(i64 id){
	// 	var jWordKV = await wordSeeker.SeekJoinedWordKVById(id);
	// 	if (jWordKV == null) {
	// 		return -1;
	// 	}
	// 	dbCtx.WordKV.Where(e=>e.id == jWordKV.textWord.id)
	// }
}
