using model;
using ngaq.Core.model.wordIF;
using ngaq.Core.svc.crud.wordCrud.IF;

namespace ngaq.Server.svc.crud.wordCrud;

public class WordQuery :
	I_WordQuery
{
	public Task deleteKv(long id) {
		throw new NotImplementedException();
	}

	public Task<I_FullWordKv> SeekFullWordByFKey(long fKey) {
		throw new NotImplementedException();
	}

	public Task<I_FullWordKv> SeekFullWordById(long id) {
		throw new NotImplementedException();
	}

	public Task updateKv(long id, Dictionary<string, object?> updateFields) {
		throw new NotImplementedException();
	}

	public Task<IEnumerable<I_KvRow>> WideSearchKvs(string input) {
		throw new NotImplementedException();
	}
}