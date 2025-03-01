using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Model;
using ngaq.Core.Model.Consts;
using ngaq.Core.Model.wordIF;
using ngaq.Model.Consts;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.FullWordKv;
using ngaq.UI.viewModels.IF;
using ngaq.UI.viewModels.KV;

namespace ngaq.UI.viewModels.WordInfo;

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


	protected i64 _id;
	public i64 id{
		get => _id;
		set => SetProperty(ref _id, value);
	}

	protected str _wordText;
	public str wordText{
		get => _wordText;
		set => SetProperty(ref _wordText, value);
	}



	protected str _lang = "";
	public str lang{
		get => _lang;
		set => SetProperty(ref _lang, value);
	}





	protected ObservableCollection<I_PropertyKv> _otherProps = new();
	public ObservableCollection<I_PropertyKv> otherProps{
		get => _otherProps;
		set => SetProperty(ref _otherProps, value);
	}


	protected zero _init(){
		id = fullWordKv.textWord.id;
		wordText = fullWordKv.textWord.kStr??"";
		lang = fullWordKv.textWord.lang_();
		//prop分類
		means.Clear();
		otherProps.Clear();
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
