using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.UI.ViewModels;

namespace ngaq.UI.ViewModels;

public partial class MainViewModel : ViewModelBase {
	[ObservableProperty]
	private string _greeting = "Welcome to Avalonia!";
}
