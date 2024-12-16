using ngaq.Core.model;
using ngaq.Core.svc.crud;
using ngaq.Server.db.crud;
using ngaq.model.consts;
using ngaq.Core.model.wordIF;
using tools.IF;
using ngaq.Server.Db.Crud.IF;
using Microsoft.EntityFrameworkCore.Storage;
using ngaq.Core.model.consts;
using model.consts;
using ngaq.Core.svc;
using db;
using Microsoft.EntityFrameworkCore;

namespace ngaq.Server.svc.crud.wordCrud;


public class WordRm:
	IDisposable
	,I_SetTx_DbCtx
	,I_Rm<I_JoinedWordKV>
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


	public async Task<unit> Rm(I_JoinedWordKV joinedWordKV){
		dbCtx.WordKV.Remove((WordKV)joinedWordKV.textWord);
		dbCtx.WordKV.RemoveRange((IList<WordKV>)joinedWordKV.propertys);
		dbCtx.WordKV.RemoveRange((IList<WordKV>)joinedWordKV.learns);
		await dbCtx.SaveChangesAsync();
		return 0;
	}

	public async Task<unit> SetTx(IDbContextTransaction transaction) {
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
