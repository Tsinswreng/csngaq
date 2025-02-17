using ngaq.Core.Model;
using ngaq.Core.Svc.Crud;
using ngaq.Server.db.crud;
using ngaq.model.consts;
using ngaq.Core.Model.wordIF;
using tools.IF;
using ngaq.Server.Db.Crud.IF;
using Microsoft.EntityFrameworkCore.Storage;
using ngaq.Core.Model.consts;
using model.consts;
using ngaq.Core.Svc;
using db;
using Microsoft.EntityFrameworkCore;

namespace ngaq.Server.Svc.Crud.WordCrud;


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
