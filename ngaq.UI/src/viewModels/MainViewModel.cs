using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.UI.viewModels;

namespace ngaq.UI.viewModels;

public partial class MainViewModel : ViewModelBase {
	[ObservableProperty]
	private string _greeting = "Welcome to Avalonia!";
}
