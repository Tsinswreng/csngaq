using ngaq.Server.db.ngaq4;
using ngaq.Core.Svc.ngaq4;
using ngaq.Core.model;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using System.Data;
using model;
using ngaq.Core.Svc.Crud;
using tools.IF;


namespace ngaq.Server.db.crud;

public class KVAdder:
	IDisposable
	, I_TxAdderAsync<I_KVRow, zero>
	,I_SetTx<IDbContextTransaction>
	,I_GetLastId<i64?>
{

	protected zero _init_sql_add(){
		sql_add =
@$"INSERT INTO {tblName} (
	{nameof(KV.bl)}
	,{nameof(KV.ct)}
	,{nameof(KV.ut)}
	,{nameof(KV.kType)}
	,{nameof(KV.kStr)}
	,{nameof(KV.kI64)}
	,{nameof(KV.kDesc)}
	,{nameof(KV.vType)}
	,{nameof(KV.vStr)}
	,{nameof(KV.vI64)}
	,{nameof(KV.vF64)}
	,{nameof(KV.vDesc)}
)VALUES (
	@{nameof(KV.bl)}
	,@{nameof(KV.ct)}
	,@{nameof(KV.ut)}
	,@{nameof(KV.kType)}
	,@{nameof(KV.kStr)}
	,@{nameof(KV.kI64)}
	,@{nameof(KV.kDesc)}
	,@{nameof(KV.vType)}
	,@{nameof(KV.vStr)}
	,@{nameof(KV.vI64)}
	,@{nameof(KV.vF64)}
	,@{nameof(KV.vDesc)}
)";
		return 0;
	}

	public str sql_add{get; protected set;}

	public str sql_lastId{get;} = "SELECT last_insert_rowid()";

	public str tblName{get;}




	public KVAdder(str tblName) {
		this.tblName = tblName;
		_init_sql_add();
	}

	~KVAdder(){
		Dispose();
	}
	public void Dispose() {
		conn?.Dispose();
		_tx?.Dispose();
		dbCtx?.Dispose();
		_cmd_add?.Dispose();
		_cmd_lastId?.Dispose();
	}

	public System.Data.Common.DbConnection conn{get; set;}

	protected System.Data.Common.DbCommand _cmd_add;
	protected System.Data.Common.DbCommand _cmd_lastId{get;set;}

	protected IDbContextTransaction? _tx{get; set;}

	protected NgaqDbCtx dbCtx = new();


	public async Task<zero> SetTx(IDbContextTransaction tx){
		_tx = tx;
		_cmd_add.Transaction = _tx.GetDbTransaction();
		_cmd_lastId.Transaction = _tx.GetDbTransaction();
		return 0;
	}

	public async Task<zero> Begin(){
		conn = dbCtx.Database.GetDbConnection();
		await conn.OpenAsync();
		_cmd_add = conn.CreateCommand();
		_cmd_add.CommandText = sql_add;
		_cmd_add.CommandType = System.Data.CommandType.Text;

		_cmd_lastId = conn.CreateCommand();
		_cmd_lastId.CommandText = sql_lastId;
		_cmd_lastId.CommandType = System.Data.CommandType.Text;

		// _tx = await dbCtx.BeginTrans();

		// _cmd_add.Transaction = _tx.GetDbTransaction(); // AI曰 如果没有这两行代码，_cmd_add 和 _cmd_lastId 命令将独立于事务执行。
		// _cmd_lastId.Transaction = _tx.GetDbTransaction();
		if(_tx !=null){
			await SetTx(_tx);
		}

		//_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.id)}", DbType.Int64));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.bl)}", DbType.String));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.ct)}", DbType.Int64));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.ut)}", DbType.Int64));

		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.kType)}", DbType.String));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.kStr)}", DbType.String));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.kI64)}", DbType.Int64));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.kDesc)}", DbType.String));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.vType)}", DbType.String));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.vStr)}", DbType.String));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.vI64)}", DbType.Int64));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.vF64)}", DbType.Double));
		_cmd_add.Parameters.Add(new SqliteParameter($"@{nameof(KV.vDesc)}", DbType.String));
		return 0;
	}

	/// <summary>
	/// null convert to DBNull.Value
	/// </summary>
	/// <param name="v"></param>
	/// <returns></returns>
	protected unknown nc<T>(T? v){
		if(v == null){
			return DBNull.Value;
		}
		return v;
	}

	public async Task<zero> TxAddAsync(I_KVRow e){
		_cmd_add.Parameters[$"@{nameof(KV.bl)}"].Value = nc(e.bl);
		_cmd_add.Parameters[$"@{nameof(KV.ct)}"].Value = nc(e.ct);
		_cmd_add.Parameters[$"@{nameof(KV.ut)}"].Value = nc(e.ut);

		_cmd_add.Parameters[$"@{nameof(KV.kType)}"].Value = nc(e.kType);
		_cmd_add.Parameters[$"@{nameof(KV.kStr)}"].Value = nc(e.kStr);
		_cmd_add.Parameters[$"@{nameof(KV.kI64)}"].Value = nc(e.kI64);
		_cmd_add.Parameters[$"@{nameof(KV.kDesc)}"].Value = nc(e.kDesc);
		_cmd_add.Parameters[$"@{nameof(KV.vType)}"].Value = nc(e.vType);
		_cmd_add.Parameters[$"@{nameof(KV.vStr)}"].Value = nc(e.vStr);
		_cmd_add.Parameters[$"@{nameof(KV.vI64)}"].Value = nc(e.vI64);
		_cmd_add.Parameters[$"@{nameof(KV.vF64)}"].Value = nc(e.vF64);
		_cmd_add.Parameters[$"@{nameof(KV.vDesc)}"].Value = nc(e.vDesc);

		await _cmd_add.ExecuteNonQueryAsync(); // 执行命令
		return 0;
	}

	public async Task<zero> Commit(){
		if(_tx !=null){await _tx.CommitAsync();}
		return 0;
	}

	public async Task<i64?> GetLastId() {
		var result = await _cmd_lastId.ExecuteScalarAsync(); // 獲取lastId
		if(result != null && result is i64){
			return (i64)result;
		}else{
			return null;
		}
	}
}
