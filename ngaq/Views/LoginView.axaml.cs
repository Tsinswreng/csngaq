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
	}



	private void LoginView_Loaded(object sender, RoutedEventArgs e){
		G.log("LoginView_Loaded");
	}

	private void Button_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e) {
		// if (TextBoxA != null && TextBoxB != null) {
		// 	TextBoxB.Text = TextBoxA.Text;
		// }不效
		try{
			G.log("start");
			G.log(TextBoxA==null); //True
			G.log(TextBoxA?.Text??""); //""
			G.log(123);

			if(this.DataContext is LoginViewModel viewModel){
				G.log(viewModel.TextBoxAText);
			}else{
				G.log("no viewmodel");
			}
		}
		catch (System.Exception ex){
			System.Console.WriteLine(ex);
		}

		//string userText = viewModel.TextBoxAText;

	}


}
