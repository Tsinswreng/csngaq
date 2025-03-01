using System.Collections.Generic;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.IF;

namespace ngaq.UI.template;

using Model = object;

public class TemplateVm
	:ViewModelBase
	,I_ViewModel<Model>
{


	public static IList<TemplateVm> samples{get;set;} = new List<TemplateVm>();
	static TemplateVm(){

	}

	public Model model{get;set;}

	public zero fromModel(Model model) {
		this.model = model;
		_init();
		return 0;
	}

	public Model toModel() {
		return model;
	}

	protected zero _init(){
		hasValue = true;
		return 0;
	}

	protected bool _hasValue;
	public bool hasValue{
		get{return _hasValue;}
		set{SetProperty(ref _hasValue, value);}
	}

}