using model.consts;
using ngaq.model.consts;

namespace ngaq.Core.model;

public static class TextWordKVExt{
	public static str text_(this I_TextWordKV o){
		return o.kStr??"";
	}

	public static str lang_(this I_TextWordKV o){
		var (prefix,lang) = BlPrefix.split(o.bl??"");
		return lang;
	}
}


public static class WidKVExt{
	public static i64 wid_(this I_widKV o){
		return o.kI64??throw new NullReferenceException("wid is null");
	}

	/// <summary>
	/// 亦設kDesc
	/// </summary>
	/// <param name="o"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static zero wid_(this I_widKV o, i64 value){
		o.setKI64(value);
		o.kDesc = KDesc.fKey.ToString();
		return 0;
	}
}


public static class LearnKVExt{
	public static str get_learnRes(this I_LearnKV o){
		return o.vStr??"";
	}

	public static zero set_learnRes(this I_LearnKV o, str value){
		//o.vStr = value;
		o.setVStr(value);
		return 0;
	}


}