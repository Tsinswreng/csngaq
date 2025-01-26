using model;

namespace ngaq.Core.model;

public interface I_WordKV : I_KVRow {

}


/// <summary>
/// KV芝kI64潙TextWord之id 且 kDesc潙fKey者 方叶此接口
/// </summary>
public interface I_widKV: I_WordKV{
//留空
}

/// <summary>
/// when bl is TextWord:<lang>
/// </summary>
public interface I_TextWordKV : I_WordKV {}

/// <summary>
/// when bl is Property:<mean|tag|...>
/// kI64用于存外鍵。Property之類型ˋ存于bl中、如Property:mean、洏非在kStr中。
/// </summary>
public interface I_PropertyKV : I_WordKV, I_widKV {}

/// <summary>
/// when bl is Learn:<null>
/// </summary>

public interface I_LearnKV : I_WordKV, I_widKV {}
