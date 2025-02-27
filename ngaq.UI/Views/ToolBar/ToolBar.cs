using Avalonia.Controls;

namespace ngaq.UI.views.toolBar;

public class ToolBar
	:UserControl
{
	public ToolBar(){

	}

	public object? ctx{
		get{return DataContext;}
		set{DataContext = value;}
	}

	protected zero _render(){
		var mainMenu = new Menu();
		Content = mainMenu;
		return 0;
	}

}