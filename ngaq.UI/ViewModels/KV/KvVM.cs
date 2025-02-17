using CommunityToolkit.Mvvm.ComponentModel;
using model.consts;
using ngaq.Core.Model;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.IF;
namespace ngaq.UI.ViewModels.KV;

public partial class KvVm
	:ViewModelBase
	,I_ViewModel<I_WordKv>
	,I_RowBaseInfo
	,I_WordKv
{
	public I_WordKv model{get;set;}

	public KvVm() {}

	public KvVm(I_WordKv model) {
		fromModel(model);
	}

	public zero fromModel(I_WordKv kv){
		model = kv;
		id = kv.id;
		bl = kv.bl;
		status = kv.status;
		ct = kv.ct;
		ut = kv.ut;
		kType = kv.kType;
		kDesc = kv.kDesc;
		kI64 = kv.kI64;
		kStr = kv.kStr;
		vType = kv.vType;
		vDesc = kv.vDesc;
		vStr = kv.vStr;
		vI64 = kv.vI64;
		vF64 = kv.vF64;
		return 0;
	}

	public I_WordKv toModel(){
		I_WordKv kv = new WordKv();
		kv.id = id;
		kv.bl = bl;
		kv.status = status;
		kv.ct = ct;
		kv.ut = ut;
		kv.kType = kType;
		kv.kDesc = kDesc;
		kv.kI64 = kI64;
		kv.kStr = kStr;
		kv.vType = vType;
		kv.vDesc = vDesc;
		kv.vStr = vStr;
		kv.vI64 = vI64;
		kv.vF64 = vF64;
		return kv;
	}

	protected i64 _id;
	public i64 id {
		get => _id;
		set => SetProperty(ref _id, value);
	}

	protected str? _bl;
	public str? bl{
		get => _bl;
		set => SetProperty(ref _bl, value);
	}

	protected i64 _ct;
	public i64 ct{
		get => _ct;
		set => SetProperty(ref _ct, value);
	}

	protected i64 _ut;
	public i64 ut{
		get => _ut;
		set => SetProperty(ref _ut, value);
	}

	protected str? _status;
	public str? status{
		get => _status;
		set => SetProperty(ref _status, value);
	}

	protected str _kType = "";
	public str kType{
		get => _kType;
		set => SetProperty(ref _kType, value);
	}

	protected str? _kDesc = "";
	public str? kDesc{
		get => _kDesc;
		set => SetProperty(ref _kDesc, value);
	}

	protected i64? _kI64;
	public i64? kI64{
		get => _kI64;
		set => SetProperty(ref _kI64, value);
	}

	protected str? _kStr;
	public str? kStr{
		get => _kStr;
		set => SetProperty(ref _kStr, value);
	}

	protected str _vType = KVType.STR.ToString();
	public str vType{
		get => _vType;
		set => SetProperty(ref _vType, value);
	}

	protected str? _vDesc;
	public str? vDesc{
		get => _vDesc;
		set => SetProperty(ref _vDesc, value);
	}




	protected str? _vStr;
	public str? vStr{
		get => _vStr;
		set => SetProperty(ref _vStr, value);
	}

	protected i64? _vI64;
	public i64? vI64{
		get => _vI64;
		set => SetProperty(ref _vI64, value);
	}

	protected f64? _vF64;
	public f64? vF64{
		get => _vF64;
		set => SetProperty(ref _vF64, value);
	}





}


