using model;

namespace ngaq.Core.model;

public interface I_WordKv : I_KvRow {

}


/// <summary>
/// KV芝kI64潙TextWord之id 且 kDesc潙fKey者 方叶此接口
/// </summary>
public interface I_widKV: I_WordKv{
//留空
}

/// <summary>
/// when bl is TextWord:<lang>
/// </summary>
public interface I_TextWordKV : I_WordKv {}

/// <summary>
/// when bl is Property:<mean|tag|...>
/// kI64用于存外鍵。Property之類型ˋ存于bl中、如Property:mean、洏非在kStr中。
/// </summary>
public interface I_PropertyKv : I_WordKv, I_widKV {}

/// <summary>
/// when bl is Learn:<null>
/// </summary>

public interface I_LearnKv : I_WordKv, I_widKV {}
