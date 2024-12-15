using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using ngaq.ViewModels;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using ngaq.ViewModels.Word;
using ngaq.Core.model.sample;

namespace ngaq.Views.Word;

public partial class WordInfoView : UserControl {
	public WordInfoView() {
		InitializeComponent();
	}

	private void InitializeComponent() {
		AvaloniaXamlLoader.Load(this);
		this.DataContext = new WordInfoViewModel();
		//<須有此、否得其DataContext潙父組件厎 則致謬
	}



	private void LoginView_Loaded(object sender, RoutedEventArgs e){
		G.log("LoginView_Loaded");
	}

	private void Button_Click(object sender, RoutedEventArgs e) {
		if(! (this.DataContext is WordInfoViewModel) ){
			return;
		}
		var v = (WordInfoViewModel)this.DataContext;
		v.upd_joinedWordKV(
			JoinedWordSample.getInst().joinedWord
		);
	}


}


