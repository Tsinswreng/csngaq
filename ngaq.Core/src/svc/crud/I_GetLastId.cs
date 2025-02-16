namespace ngaq.Core.Svc.Crud;

public interface I_GetLastId<T>{
	Task<T> GetLastId();
}