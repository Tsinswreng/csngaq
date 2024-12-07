using ngaq.Core.model;
using ngaq.Core.svc.crud;
using ngaq.Server.db.crud;
using ngaq.model.consts;
using ngaq.Core.model.wordIF;
using tools.IF;

namespace ngaq.Server.svc.crud.wordCrud;


public class WordAdder:
	I_TxAdderAsync<IList<I_WordKV>>
	, IDisposable
	,I_SetTx<>
{

	public WordAdder() {

	}

	~WordAdder(){
		Dispose();
	}
	public void Dispose() {
		_kvAdder.Dispose();
	}


	protected KVAdder _kvAdder = new(Opt.inst().tblName_WordKV);

	public Task<unit> Begin() {
		return _kvAdder.Begin();
	}

	public Task<byte> Commit() {
		return _kvAdder.Commit();
	}

	public async Task<long?> TxAddAsync(IList<I_JoinedWordKV> words) {
		var wordSeeker = new WordSeeker();
		var kvAdder = new KVAdder(Opt.inst().tblName_WordKV);
		foreach(var o in words){
			var got = await wordSeeker.SeekJoinedWordKVByTextEtBl(o.textWord.kStr??"", o.textWord.bl);
			if(got == null){ //表中原無此詞則直ᵈ添
				kvAdder.
			}
		}


	}


}