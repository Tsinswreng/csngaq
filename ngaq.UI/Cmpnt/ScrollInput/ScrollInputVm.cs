using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.UI.viewModels;

namespace ngaq.UI.Cmpnt.ScrollInput;

public partial class ScrollInputVm: ViewModelBase{
	protected str _Text = "";
	public str Text {
		get => _Text;
		set => SetProperty(ref _Text, value);
	}
}

