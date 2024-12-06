// using ngaq.Core.model;
// using ngaq.Core.svc.crud;
// using ngaq.model.consts;

// namespace ngaq.Server.svc.crud.wordCrud;


// public class WordAdder:I_TxAdderAsync<I_WordKV>, IDisposable{

// 	public WordAdder() {

// 	}

// 	~WordAdder(){
// 		Dispose();
// 	}
// 	public void Dispose() {
// 		_kvAdder.Dispose();
// 	}


// 	protected KVAdder _kvAdder = new();

// 	public Task<unit> Begin() {
// 		return _kvAdder.Begin();
// 	}

// 	public Task<byte> Commit() {
// 		return _kvAdder.Commit();
// 	}

// 	public Task<long?> TxAddAsync(I_WordKV o) {

// 		if(o.bl.StartsWith(BlPrefix.TextWord)){

// 		}
// 	}


// }