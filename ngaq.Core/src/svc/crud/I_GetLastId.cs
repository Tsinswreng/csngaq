namespace ngaq.Core.svc.crud;

public interface I_GetLastId<T>{
	Task<T> GetLastId();
}