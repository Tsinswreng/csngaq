using tools.IF;
namespace ngaq.Core.svc.crud;


public interface I_TxAdderAsync<T> :I_Transaction{
	/// <summary>
	///
	/// </summary>
	/// <param name="entity"></param>
	/// <returns>lastID</returns>
	Task<i64?> TxAddAsync(T entity);
}