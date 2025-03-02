
using model;
using model.consts;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.IF;
namespace ngaq.UI.viewModels.kv;
public class KvVm2
	:ViewModelBase
	,I_ViewModel<I_KvRow>
{
	public zero fromModel(I_KvRow model) {
		this.model = model;
		hasValue = true;
		_init();
		return 0;
	}

	public I_KvRow toModel() {
		return model;
	}

	public I_KvRow model{get;set;}


	protected zero _init(){
		id = model.id;
		bl = model.bl;
		status = model.status;
		ct = model.ct;
		ut = model.ut;
		kType = model.kType;
		kDesc = model.kDesc;
		kI64 = model.kI64;
		kStr = model.kStr;
		vType = model.vType;
		vDesc = model.vDesc;
		vStr = model.vStr;
		vI64 = model.vI64;
		vF64 = model.vF64;
		return 0;
	}


	protected bool _hasValue = false;
	public bool hasValue{
		get{return _hasValue;}
		set{SetProperty(ref _hasValue, value);}
	}


	protected i64 _id;
	public i64 id {
		get => _id;
		set{
			model.id = value;
			SetProperty(ref _id, value);
		}
	}

	protected str? _bl;
	public str? bl{
		get => _bl;
		set{
			model.bl = value;
			SetProperty(ref _bl, value);
		}
	}

	protected i64 _ct;
	public i64 ct{
		get => _ct;
		set{
			model.ct = value;
			SetProperty(ref _ct, value);
		}
	}

	protected i64 _ut;
	public i64 ut{
		get => _ut;
		set{
			model.ut = value;
			SetProperty(ref _ut, value);
		}
	}

	protected str? _status;
	public str? status{
		get => _status;
		set{
			model.status = value;
			SetProperty(ref _status, value);
		}
	}

	protected str _kType = "";
	public str kType{
		get => _kType;
		set{
			model.kType = value;
			SetProperty(ref _kType, value);
		}
	}

	protected str? _kDesc = "";
	public str? kDesc{
		get => _kDesc;
		set{
			model.kDesc = value;
			SetProperty(ref _kDesc, value);
		}
	}

	protected i64? _kI64;
	public i64? kI64{
		get => _kI64;
		set{
			model.kI64 = value;
			SetProperty(ref _kI64, value);
		}
	}

	protected str? _kStr;
	public str? kStr{
		get => _kStr;
		set{
			model.kStr = value;
			SetProperty(ref _kStr, value);
		}
	}

	protected str _vType = KVType.STR.ToString();
	public str vType{
		get => _vType;
		set{
			model.vType = value;
			SetProperty(ref _vType, value);
		}
	}

	protected str? _vDesc;
	public str? vDesc{
		get => _vDesc;
		set{
			model.vDesc = value;
			SetProperty(ref _vDesc, value);
		}
	}


	protected str? _vStr;
	public str? vStr{
		get => _vStr;
		set{
			model.vStr = value;
			SetProperty(ref _vStr, value);
		}
	}

	protected i64? _vI64;
	public i64? vI64{
		get => _vI64;
		set{
			model.vI64 = value;
			SetProperty(ref _vI64, value);
		}
	}

	protected f64? _vF64;
	public f64? vF64{
		get => _vF64;
		set{
			model.vF64 = value;
			SetProperty(ref _vF64, value);
		}
	}

}