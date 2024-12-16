namespace ngaq.Core.svc.crud;

public interface I_Add<T>{
	Task<unit> Add(T entity);
}

public interface I_Rm<T>{
	Task<unit> Rm(T entity);
}

public interface I_Upd<T>{
	Task<unit> Upd(T entity);
}
