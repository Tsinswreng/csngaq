using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.UI.ViewModels;

namespace ngaq.UI.ViewModels;

public partial class LoginViewModel : ViewModelBase {
	[ObservableProperty]
	private string _textBoxAText = "Welcome to Avalonia!";


	[ObservableProperty]
	private string _textBoxBText = "";
}
