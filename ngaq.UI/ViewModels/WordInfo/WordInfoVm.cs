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
		fullWordKvVm.fromModel(model);
		return 0;
	}

	public I_FullWordKv toModel() {
		return fullWordKvVm.toModel();
	}


	protected FullWordKvVm _fullWordKvVm = new FullWordKvVm();
	public FullWordKvVm fullWordKvVm{
		get => _fullWordKvVm;
		set => SetProperty(ref _fullWordKvVm, value);
	}

	protected ObservableCollection<KvVm> _meanVms = new();
	public ObservableCollection<KvVm> meanVms{
		get => _meanVms;
		set => SetProperty(ref _meanVms, value);
	}


	protected ObservableCollection<KvVm> _otherPropVms = new();
	public ObservableCollection<KvVm> otherPropVms{
		get => _otherPropVms;
		set => SetProperty(ref _otherPropVms, value);
	}


	protected zero _count(){
		//prop分類
		foreach(var propVm in fullWordKvVm.propertyVms){
			if(propVm.bl == BlPrefix.join(BlPrefix.Property, PropertyEnum.mean.ToString())){
				meanVms.Add(propVm);
			}else{
				otherPropVms.Add(propVm);
			}
		}




		return 0;
	}


}