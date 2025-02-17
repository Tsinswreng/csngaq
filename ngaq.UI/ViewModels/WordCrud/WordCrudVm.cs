using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.UI.ViewModels.FullWordKv;

namespace ngaq.UI.ViewModels.WordCrud;

public partial class WordCrudVm
	:ViewModelBase
{


	protected str _searchId="init";
	public str searchId{
		get => _searchId;
		set => SetProperty(ref _searchId, value);
	}

	protected FullWordKvVm _fullWordKvVm = new FullWordKvVm();
	public FullWordKvVm fullWordKvVm{
		get => _fullWordKvVm;
		set => SetProperty(ref _fullWordKvVm, value);
	}

	



}