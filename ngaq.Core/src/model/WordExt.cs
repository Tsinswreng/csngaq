using model.consts;
using ngaq.model.consts;

namespace ngaq.Core.model;

public static class TextWordKvExt{
	public static str text_(this I_TextWordKV z){
		return z.kStr??"";
	}

	public static zero text_(this I_TextWordKV z, str value){
		z.kStr_(value);
		return 0;
	}

	public static str lang_(this I_TextWordKV z){
		var (prefix,lang) = BlPrefix.split(z.bl??"");
		return lang;
	}

	public static zero lang_(this I_TextWordKV z, str lang){
		var bl = BlPrefix.join(
			BlPrefix.TextWord
			,lang
		);
		z.bl = bl;
		return 0;
	}
}


public static class WidKVExt{
	public static i64 wid_(this I_widKV z){
		return z.kI64??throw new NullReferenceException("wid is null");
	}

	/// <summary>
	/// 亦設kDesc
	/// </summary>
	/// <param name="z"></param>
	/// <param name="value"></param>
	/// <returns></returns>
	public static zero wid_(this I_widKV z, i64 value){
		z.kI64_(value);
		z.kDesc = KDesc.fKey.ToString();
		return 0;
	}
}


public static class LearnKvExt{
	public static str learnResult_(this I_LearnKV o){
		return o.vStr??"";
	}

	public static zero learnResult_(this I_LearnKV o, str value){
		//o.vStr = value;
		o.vStr_(value);
		return 0;
	}
}


public static class PropertyKvExt{
	public static zero setStr(this I_PropertyKV z, str bl, str val){
		z.bl = BlPrefix.join(BlPrefix.Property,bl);
		z.vStr_(val);
		return 0;
	}
}