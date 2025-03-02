///舊版單詞對象轉換為新版Key-Value格式
using ngaq.Core.model.ngaq4;
using ngaq.Core.model;
using model;
using ngaq.Model.Consts;
using model.consts;
namespace ngaq.Core.svc.ngaq4;
public class Ngaq4ModToWordKV{

	public zero assignIdCtMt(I_RowBaseInfo target, IdBlCtMt4 idBlCtMt){
		target.id = idBlCtMt.id;
		target.ct = idBlCtMt.ct;
		target.ut = idBlCtMt.mt;
		return 0;
	}

	public I_KvRow convertTextWord(TextWord4 o){
		I_KvRow kv = new WordKv();
		assignIdCtMt(kv, o);
		kv.kStr = o.text;
		kv.bl = BlPrefix.join(BlPrefix.TextWord, o.belong);
		return kv;
	}

	public I_KvRow convertProperty(Property4 o){
		I_KvRow kv = new WordKv();
		assignIdCtMt(kv, o);
		kv.vStr_(o.text);
		kv.bl = BlPrefix.join(BlPrefix.Property, o.belong);
		kv.kI64_(o.wid);
		kv.kDesc = KDesc.fKey.ToString();
		return kv;
	}

	public I_KvRow convertLearn(Learn4 o){
		I_KvRow kv = new WordKv();
		assignIdCtMt(kv, o);
		kv.bl = BlPrefix.join(BlPrefix.Learn, "");
		kv.vStr_(o.belong);
		kv.kI64_(o.wid);

		kv.kDesc = KDesc.fKey.ToString();
		return kv;
	}
}
