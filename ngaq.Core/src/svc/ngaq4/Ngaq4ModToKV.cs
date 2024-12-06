///舊版單詞對象轉換為新版Key-Value格式
using ngaq.Core.model.ngaq4;
using ngaq.Core.model;
using model;
using ngaq.model.consts;
using model.consts;
namespace ngaq.Core.svc.ngaq4;
public class Ngaq4ModToWordKV{

	public unit assignIdBlCtMt(I_IdBlCtUt target, IdBlCtMt idBlCtMt){
		target.id = idBlCtMt.id;
		target.bl = idBlCtMt.bl;
		target.ct = idBlCtMt.ct;
		target.ut = idBlCtMt.mt;
		return 0;
	}

	public I_KVIdBlCtUt convertTextWord(TextWord o){
		I_KVIdBlCtUt kv = new WordKV();
		assignIdBlCtMt(kv, o);
		kv.kStr = o.text;
		kv.bl = BlPrefix.join(BlPrefix.TextWord, o.bl);
		return kv;
	}

	public I_KVIdBlCtUt convertProperty(Property o){
		I_KVIdBlCtUt kv = new WordKV();
		assignIdBlCtMt(kv, o);
		kv.setVStr(o.text);
		kv.bl = BlPrefix.join(BlPrefix.Property, o.bl);
		kv.setKI64(o.wid);
		kv.kDesc = KDesc.fKey.ToString();
		return kv;
	}

	public I_KVIdBlCtUt convertLearn(Learn o){
		I_KVIdBlCtUt kv = new WordKV();
		assignIdBlCtMt(kv, o);
		kv.bl = BlPrefix.join(BlPrefix.Learn, "");
		kv.setVStr(o.bl);
		kv.setKI64(o.wid);
		kv.kDesc = KDesc.fKey.ToString();
		return kv;
	}
}
