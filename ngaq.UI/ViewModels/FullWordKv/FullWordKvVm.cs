using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.model;
using ngaq.Core.model.sample;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.ViewModels.FullWordKv;

public class Person{
	public str Name{get;set;} = "";
	public int Age{get;set;} = 0;
}

public partial class FullWordKvVm: ViewModelBase{

	//試 未成
	// protected I_WordKv _textWord = FullWordSample.getInst().sample.textWord;
	// public I_WordKv textWord{
	// 	get => _textWord;
	// 	set => SetProperty(ref _textWord, value);
	// }

	protected KvVm _textWordVm = new KvVm(FullWordSample.getInst().sample.textWord);
	public KvVm textWordVm{
		get => _textWordVm;
		set => SetProperty(ref _textWordVm, value);
	}

	protected ObservableCollection<KvVm> _propertyVms
		= new(FullWordSample.getInst().sample.propertys.Select(e=>new KvVm(e)).ToList())
	;
	public ObservableCollection<KvVm> propertyVms{
		get => _propertyVms;
		set => SetProperty(ref _propertyVms, value);
	}


	protected IList<KvVm> _learnVms
		= FullWordSample.getInst().sample.learns.Select(e=>new KvVm(e)).ToList()
	;
	public IList<KvVm> learnVms{
		get => _learnVms;
		set => SetProperty(ref _learnVms, value);
	}

	protected ObservableCollection<Person> _persons = [
		new Person{Name="Alice", Age=25},
		new Person{Name="Bob", Age=30},
	];
	public ObservableCollection<Person> persons{
		get => _persons;
		set => SetProperty(ref _persons, value);
	}

}

