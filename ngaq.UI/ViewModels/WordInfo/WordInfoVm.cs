using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Model.wordIF;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.FullWordKv;
using ngaq.UI.ViewModels.IF;

namespace ngaq.UI.ViewModels.WordInfo;

public partial class WordInfoVm
	: ViewModelBase
{
	protected FullWordKvVm _fullWordKvVm = new FullWordKvVm();
	public FullWordKvVm fullWordKvVm{
		get => _fullWordKvVm;
		set => SetProperty(ref _fullWordKvVm, value);
	}

	




}