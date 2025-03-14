using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.UI.viewModels;

namespace ngaq.UI.viewModels;

public partial class LoginViewModel : ViewModelBase {
	[ObservableProperty]
	private string _textBoxAText = "Welcome to Avalonia!";


	[ObservableProperty]
	private string _textBoxBText = "";
}
