using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using ngaq.ViewModels;

namespace ngaq.Views;

public partial class LoginView : UserControl {
	public LoginView() {
		InitializeComponent();
	}

	private void InitializeComponent() {
		AvaloniaXamlLoader.Load(this);
		this.DataContext = new LoginViewModel();//t
	}



	private void LoginView_Loaded(object sender, RoutedEventArgs e){
		G.log("LoginView_Loaded");
	}

	private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e) {

		if(! (this.DataContext is LoginViewModel) ){
			return;
		}
		var v = (LoginViewModel)this.DataContext;
		G.log(v.TextBoxAText);
		G.log(v.TextBoxBText);
		v.TextBoxBText = v.TextBoxAText;
	}


}
