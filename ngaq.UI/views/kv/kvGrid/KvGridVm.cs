
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using model;
using model.consts;
using ngaq.Core.Model.Sample;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.IF;


namespace ngaq.UI.views.kv.kvGrid;
public class KvGridVm
	: ViewModelBase
	, I_ViewModel<IEnumerable<I_KvRow>>
{

	public static IList<KvGridVm> samples{get;set;} = new List<KvGridVm>();
	static KvGridVm(){
		var fullWord = FullWordSample.getInst().sample;
		{

			var kvGridVm = new KvGridVm();
			kvGridVm.fromModel([fullWord.textWord]);
			samples.Add(kvGridVm);
		}
		{
			var kvGridVm = new KvGridVm();
			kvGridVm.fromModel(fullWord.propertys);
			samples.Add(kvGridVm);
		}
		{
			var kvGridVm = new KvGridVm();
			kvGridVm.fromModel(fullWord.learns);
			samples.Add(kvGridVm);
		}
	}



	public zero fromModel(IEnumerable<I_KvRow> model) {
		this.model = model;
		_init();
		return 0;
	}

	public IEnumerable<I_KvRow> toModel() {
		throw new System.NotImplementedException();
	}

	public IEnumerable<I_KvRow> model{get;set;}


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