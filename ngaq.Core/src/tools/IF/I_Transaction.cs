namespace tools.IF;
public interface I_Transaction{
	Task<zero> Begin();
	Task<zero> Commit();
}

public interface I_SetTx<T>{
	Task<zero> SetTx(T transaction);
}