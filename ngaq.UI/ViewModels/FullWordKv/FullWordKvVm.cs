using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.model;
using ngaq.Core.model.sample;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.ViewModels.FullWordKv;

public partial class FullWordKv: ViewModelBase{

	protected I_WordKv _textWord = FullWordSample.getInst().sample.textWord;
	public I_WordKv textWord{
		get => _textWord;
		set => SetProperty(ref _textWord, value);
	}
}

