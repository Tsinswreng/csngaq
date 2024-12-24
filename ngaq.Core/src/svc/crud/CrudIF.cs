namespace ngaq.Core.svc.crud;

public interface I_Add<T>{
	Task<zero> Add(T entity);
}

public interface I_Rm<T>{
	Task<zero> Rm(T entity);
}

public interface I_Upd<T>{
	Task<zero> Upd(T entity);
}
