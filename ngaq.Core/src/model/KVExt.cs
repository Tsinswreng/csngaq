///KVExt 类提供了扩展方法，用于设置实现 I_KV 接口的对象的键值对的字符串和64位整数类型。
using model.consts;

namespace ngaq.Core.model;

public static class KVExt {

	public static zero kStr_(this I_KV kv, str kStr) {
		kv.kStr = kStr;
		kv.kType = KVType.STR.ToString();
		return 0;
	}

	public static zero kI64_(this I_KV kv, i64 kI64) {
		kv.kI64 = kI64;
		kv.kType = KVType.I64.ToString();
		return 0;
	}

	public static zero vStr_(this I_KV kv, str vStr) {
		kv.vStr = vStr;
		kv.vType = KVType.STR.ToString();
		return 0;
	}

	public static zero vI64_(this I_KV kv, i64 vI64) {
		kv.vI64 = vI64;
		kv.vType = KVType.I64.ToString();
		return 0;
	}
}
