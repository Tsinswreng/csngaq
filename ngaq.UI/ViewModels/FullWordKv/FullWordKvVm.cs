using Avalonia.Interactivity;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.model;
using ngaq.Core.model.sample;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.ViewModels.FullWordKv;

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

	



}

