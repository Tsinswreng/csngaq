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

namespace ngaq.Server.svc.crud.wordCrud;


public class WordRm: IDisposable {
	public WordRm() {

	}

	~WordRm(){
		Dispose();
	}

	WordSeeker wordSeeker = new ();

	NgaqDbCtx dbCtx = new ();

	public void Dispose() {

	}

	// public async Task<code> RmById(i64 id){
	// 	var jWordKV = await wordSeeker.SeekJoinedWordKVById(id);
	// 	if (jWordKV == null) {
	// 		return -1;
	// 	}
	// 	dbCtx.WordKV.Where(e=>e.id == jWordKV.textWord.id)
	// }
}
