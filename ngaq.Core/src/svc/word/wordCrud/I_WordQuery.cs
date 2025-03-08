using model;
using ngaq.Core.model.wordIF;
namespace ngaq.Core.svc.crud.wordCrud.IF;
public interface I_WordQuery{
	/// <summary>
	/// 寬ᵈ尋 既可由id亦可由kStr
	/// </summary>
	/// <param name="input"></param>
	/// <returns></returns>
	public Task< IEnumerable<I_KvRow> > WideSearchKvs(str input);

	public Task<I_FullWordKv> SeekFullWordById(i64 id);

	public Task<I_FullWordKv> SeekFullWordByFKey(i64 fKey);

	public Task updateKv(
		i64 id
		,Dictionary<str, object?> updateFields
	);

	public Task deleteKv(i64 id);

}

