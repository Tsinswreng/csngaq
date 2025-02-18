using Avalonia.Controls;
using ngaq.UI.Views.WordInfo;
namespace ngaq.UI.Views;

public partial class MainView : UserControl{
	public MainView(){
		//InitializeComponent();
		Content = new WordInfoView();
	}
}
