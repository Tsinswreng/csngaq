using Avalonia.Controls;

namespace ngaq.UI.views;

public partial class MainWindow : Window {
	public MainWindow() {
		//InitializeComponent();
		Content = new MainView();
	}
}
