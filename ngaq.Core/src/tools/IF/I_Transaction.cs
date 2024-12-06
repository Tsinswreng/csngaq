namespace tools.IF;
public interface I_Transaction{
	Task<unit> Begin();
	Task<unit> Commit();
}