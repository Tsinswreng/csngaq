using model;

namespace ngaq.Core.model;

public interface I_WordKV : I_KVIdBlCtUt {

}

/// <summary>
/// when bl is TextWord
/// </summary>
public interface I_TextWordKV : I_WordKV {}

/// <summary>
/// when bl is Property
/// </summary>
public interface I_PropertyKV : I_WordKV {}

/// <summary>
/// when bl is Learn
/// </summary>

public interface I_LearnKV : I_WordKV {}
