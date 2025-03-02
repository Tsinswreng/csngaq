namespace ngaq.Core.svc.crud;

public interface I_AddAsy<T>{
	Task<zero> AddAsy(T entity);
}

public interface I_RmAsy<T>{
	Task<zero> RmAsy(T entity);
}

public interface I_UpdAsy<T>{
	Task<zero> UpdAsy(T entity);
}

public interface I_SeekByIdAsy<T_id, T_rtn>{
	Task<T_rtn> SeekByIdAsy(T_id id);
}

public interface I_SeekByI64IdAsy<T_rtn>:
	I_SeekByIdAsy<i64, T_rtn>
{

}

