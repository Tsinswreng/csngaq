using Avalonia.Controls;
using Avalonia.Interactivity;

using Avalonia.Logging;
using System.Diagnostics;

namespace ngaq.Views;

public partial class MainView : UserControl{
	public MainView(){
		//Logger.TryGet(LogEventLevel.Information, LogArea.Control)?.Log(this, "123");
		InitializeComponent();
	}

	public void Button_Click(object sender, RoutedEventArgs e){
		
	}
}