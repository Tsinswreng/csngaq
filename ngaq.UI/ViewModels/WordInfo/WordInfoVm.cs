using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Model;
using ngaq.Core.Model.consts;
using ngaq.Core.Model.wordIF;
using ngaq.model.consts;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.FullWordKv;
using ngaq.UI.ViewModels.IF;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.ViewModels.WordInfo;

public partial class WordInfoVm
	:ViewModelBase
	,I_ViewModel<I_FullWordKv>
{




	public zero fromModel(I_FullWordKv model) {
		fullWordKv = model;
		_init();
		return 0;
	}

	public I_FullWordKv toModel() {
		return fullWordKv;
	}

	public I_FullWordKv fullWordKv{get;set;} = null!;

	// protected FullWordKvVm _fullWordKvVm = new FullWordKvVm();
	// public FullWordKvVm fullWordKvVm{
	// 	get => _fullWordKvVm;
	// 	set => SetProperty(ref _fullWordKvVm, value);
	// }

	protected ObservableCollection<I_PropertyKv> _means = new();
	public ObservableCollection<I_PropertyKv> means{
		get => _means;
		set => SetProperty(ref _means, value);
	}


	protected ObservableCollection<I_PropertyKv> _otherProps = new();
	public ObservableCollection<I_PropertyKv> otherProps{
		get => _otherProps;
		set => SetProperty(ref _otherProps, value);
	}


	protected zero _init(){
		//prop分類
		foreach(var propKv in fullWordKv.propertys){
			if(propKv.bl == BlPrefix.join(BlPrefix.Property, PropertyEnum.mean.ToString())){
				means.Add(propKv);
			}else{
				otherProps.Add(propKv);
			}
		}

		return 0;
	}


}