namespace tools.IF;

public interface I_Iter<out T>{
	T getNext();
	bool hasNext();
}

