using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ngaq.UI.Views.KV;
using ngaq.UI.Views.WordCrud;
using ngaq.UI.Views.WordInfo;
namespace ngaq.UI.Views;

public partial class MainView : UserControl{
	public MainView(){
		//InitializeComponent();
		//Content = new WordInfoView();
		//Content = new KvView();
		Content = new WordCrudView();
	}
}
