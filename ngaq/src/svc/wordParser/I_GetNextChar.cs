namespace ngaq.svc.wordParser;

public interface I_GetNextChar_str{
	Task<string?> GetNextChar();
}

public interface I_GetNextChar_byte{
	Task<byte> GetNextChar();
}

/* 
我的詞法解析器需要依賴一個讀字符的對象、我設計了兩種接口。用哪個好?
 */