using tools.IF;
namespace ngaq.Core.Svc.Crud;


public interface I_TxAdderAsync<Arg, Rtn> :I_Transaction{
	/// <summary>
	///
	/// </summary>
	/// <param name="entity"></param>
	/// <returns>lastID</returns>
	Task<Rtn> TxAddAsync(Arg entity);
}