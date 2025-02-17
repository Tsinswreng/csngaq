///舊版單詞對象轉換為新版Key-Value格式
using ngaq.Core.Model.ngaq4;
using ngaq.Core.Model;
using model;
using ngaq.model.consts;
using model.consts;
namespace ngaq.Core.Svc.ngaq4;
public class Ngaq4ModToWordKV{

	public zero assignIdCtMt(I_RowBaseInfo target, IdBlCtMt4 idBlCtMt){
		target.id = idBlCtMt.id;
		target.ct = idBlCtMt.ct;
		target.ut = idBlCtMt.mt;
		return 0;
	}

	public I_KVRow convertTextWord(TextWord4 o){
		I_KVRow kv = new WordKv();
		assignIdCtMt(kv, o);
		kv.kStr = o.text;
		kv.bl = BlPrefix.join(BlPrefix.TextWord, o.belong);
		return kv;
	}

	public I_KVRow convertProperty(Property4 o){
		I_KVRow kv = new WordKv();
		assignIdCtMt(kv, o);
		kv.vStr_(o.text);
		kv.bl = BlPrefix.join(BlPrefix.Property, o.belong);
		kv.kI64_(o.wid);
		kv.kDesc = KDesc.fKey.ToString();
		return kv;
	}

	public I_KVRow convertLearn(Learn4 o){
		I_KVRow kv = new WordKv();
		assignIdCtMt(kv, o);
		kv.bl = BlPrefix.join(BlPrefix.Learn, "");
		kv.vStr_(o.belong);
		kv.kI64_(o.wid);

		kv.kDesc = KDesc.fKey.ToString();
		return kv;
	}
}
