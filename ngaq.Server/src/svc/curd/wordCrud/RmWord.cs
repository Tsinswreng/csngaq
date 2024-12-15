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
{
	public WordRm() {

	}

	~WordRm(){
		Dispose();
	}

	WordSeeker wordSeeker = new ();

	NgaqDbCtx dbCtx = new ();

	public void Dispose() {

	}

	public async Task<unit> RmById(i64 id){
		var joinedWordKV = await wordSeeker.SeekJoinedWordKVById(id);
		if (joinedWordKV == null) {
			return 0;
		}
		dbCtx.WordKV.Remove((WordKV)joinedWordKV.textWord);
		dbCtx.WordKV.RemoveRange((IList<WordKV>)joinedWordKV.propertys);//?
		dbCtx.WordKV.RemoveRange((IList<WordKV>)joinedWordKV.learns);//?
		//TODO saveChange? state=deleted?  _dbContext.Entry(entityToDelete).State = EntityState.Deleted;
		throw new NotImplementedException();
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
