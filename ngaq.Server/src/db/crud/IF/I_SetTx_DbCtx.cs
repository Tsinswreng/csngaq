using Microsoft.EntityFrameworkCore.Storage;
using tools.IF;

namespace ngaq.Server.Db.Crud.IF;

public interface I_SetTx_DbCtx:
	I_SetTx<IDbContextTransaction>
{

}