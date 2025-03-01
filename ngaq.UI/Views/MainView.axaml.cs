using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using ngaq.UI.kv.kvGrid;
using ngaq.UI.views.kv.kvGrid;
using ngaq.UI.views.kv;
using ngaq.UI.views.toolBar;
using ngaq.UI.views.WordCrud;
using ngaq.UI.views.wordInfo;
using ngaq.UI.views.wordQueryPanel;
namespace ngaq.UI.views;

public partial class MainView : UserControl{
	public MainView(){
		//InitializeComponent();
		//Content = new WordInfoView();
		//Content = new KvView();
		//Content = new WordCrudView();
		//Content = new ToolBar();
		Content = new WordQueryPanel();
		//Content = new SearchedWordCard();
		//Content = new KvGrid();
		//Content = new WordInfo();
		// var o = new AutoCompleteBox{
		// 	HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch
		// };
		// var win = new Window{};
		// win.Content = o;
		// win.Show();


	}
}
