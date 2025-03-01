
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using model;
using model.consts;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.IF;
namespace ngaq.UI.views.kv.kvGrid;
public class KvGridVm
	: ViewModelBase
	, I_ViewModel<IList<I_KvRow>>
{
	public zero fromModel(IList<I_KvRow> model) {
		this.model = model;
		_init();
		return 0;
	}

	public IList<I_KvRow> toModel() {
		throw new System.NotImplementedException();
	}

	public IList<I_KvRow> model{get;set;}


	protected zero _init(){
		hasValue = true;
		kvs = new (model);
		return 0;
	}


	protected bool _hasValue = false;
	public bool hasValue{
		get{return _hasValue;}
		set{SetProperty(ref _hasValue, value);}
	}

	protected ObservableCollection<I_KvRow> _kvs=new();
	public ObservableCollection<I_KvRow> kvs{
		get{return _kvs;}
		set{SetProperty(ref _kvs, value);}
	}

}