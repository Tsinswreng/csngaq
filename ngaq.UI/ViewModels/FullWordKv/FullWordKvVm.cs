using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Model;
using ngaq.Core.Model.Sample;
using ngaq.Core.Model.wordIF;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.IF;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.ViewModels.FullWordKv;

public class Person{
	public str Name{get;set;} = "";
	public int Age{get;set;} = 0;
}


public partial class FullWordKvVm
	:ViewModelBase
	,I_ViewModel<I_FullWordKv>
{

	//試 未成
	// protected I_WordKv _textWord = FullWordSample.getInst().sample.textWord;
	// public I_WordKv textWord{
	// 	get => _textWord;
	// 	set => SetProperty(ref _textWord, value);
	// }


	protected I_FullWordKv? _model{get;set;}

	#region impl

	public zero fromModel(I_FullWordKv model) {
		this._model = model;
		//textWordVm.fromModel(model.textWord);
		textWordVm = new KvVm(model.textWord);
		propertyVms = [.. model.propertys.Select(e=>new KvVm(e)).ToList()];
		learnVms = new (model.learns.Select(e=>new KvVm(e)).ToList());
		return 0;
	}

	public I_FullWordKv toModel() {
		if(_model == null){
			_model = new FullWord();
		}
		_model.textWord = (I_TextWordKV)textWordVm.toModel();
		_model.propertys = (IList<I_PropertyKv>)propertyVms.Select(e=>e.toModel()).ToList();
		_model.learns = (IList<I_LearnKv>)learnVms.Select(e=>e.toModel()).ToList();
		return _model;
	}

	#endregion

	//protected KvVm _textWordVm = new KvVm(FullWordSample.getInst().sample.textWord);
	protected KvVm _textWordVm = new KvVm();
	public KvVm textWordVm{
		get => _textWordVm;
		set => SetProperty(ref _textWordVm, value);
	}

	protected ObservableCollection<KvVm> _propertyVms = new();
		//= new(FullWordSample.getInst().sample.propertys.Select(e=>new KvVm(e)).ToList());

	public ObservableCollection<KvVm> propertyVms{
		get => _propertyVms;
		set{
			SetProperty(ref _propertyVms, value);
		}
	}

	
	public u64 index_propertyVms{get;set;}=0;

	protected ObservableCollection<KvVm> _learnVms = new();
		//= new(FullWordSample.getInst().sample.learns.Select(e=>new KvVm(e)).ToList());
	public ObservableCollection<KvVm> learnVms{
		get => _learnVms;
		set => SetProperty(ref _learnVms, value);
	}


	// protected IList<KvVm> _learnVms
	// 	= FullWordSample.getInst().sample.learns.Select(e=>new KvVm(e)).ToList()
	// ;

	// public IList<KvVm> learnVms{
	// 	get => _learnVms;
	// 	set => SetProperty(ref _learnVms, value);
	// }

//test
	// protected ObservableCollection<Person> _persons = [
	// 	new Person{Name="Alice", Age=25},
	// 	new Person{Name="Bob", Age=30},
	// ];
	// public ObservableCollection<Person> persons{
	// 	get => _persons;
	// 	set => SetProperty(ref _persons, value);
	// }


}

