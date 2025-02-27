using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using ngaq.UI.Views.aboutWindow;
using Shr.Avalonia.Ext;

namespace ngaq.UI.views.toolBar;

public class ToolBar
	:UserControl
{
	public ToolBar(){
		_style();
		_render();
	}

	public object? ctx{
		get{return DataContext;}
		set{DataContext = value;}
	}

	protected zero _style(){
		var noRoundCorner = new Style(x=>
			x.Is<TemplatedControl>()
		);
		noRoundCorner.set(
			TemplatedControl.CornerRadiusProperty
			,new CornerRadius(0)
		);
		return 0;
	}

	protected zero _render(){
		var mainMenu = new Menu(){
			VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
		};
		Content = mainMenu;
		{{
			var file = new MenuItem{
				Header = "_File",
			};
			mainMenu.Items.Add(file);

			var help = new MenuItem{
				Header = "_Help",
			};
			mainMenu.Items.Add(help);
			{{
				var about = new MenuItem{
					Header = "_About",
				};
				help.Items.Add(about);
				{
					about.Click += (a,b)=>{
						new AboutWindow().Show();
					};
				}
			}}//~help:MenuItem

		}}//~mainMenu:Menu

		return 0;
	}

}