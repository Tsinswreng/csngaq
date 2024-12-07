namespace tools.IF;
public interface I_Transaction{
	Task<unit> Begin();
	Task<unit> Commit();
}

public interface I_SetTx<T>{
	Task<unit> SetTx(T transaction);
}