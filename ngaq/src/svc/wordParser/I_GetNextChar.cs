using System;

namespace ngaq.svc.wordParser;

public interface I_GetNextChar_str{
	Task<string?> GetNextChar();
}

[Obsolete]
public interface I_GetNextChar_i32{
	// 用 < 0 示空
	Task<i32> GetNextChar();
}

[Obsolete]
public interface I_GetNextByteNil{
	Task<byte?> GetNextChar();
}


public interface I_GetNextByte{
	Task<byte> GetNextByte();
	//Task<bool> HasNext();
	bool hasNext();
}