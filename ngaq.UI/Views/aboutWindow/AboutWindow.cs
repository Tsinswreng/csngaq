using Avalonia.Controls;

namespace ngaq.UI.Views.aboutWindow;

public partial class AboutWindow
	:Window
{
	public AboutWindow(){
		Width = 300;
		Height = 200;
		Title = "About";
		_render();
	}


	protected zero _render(){
		var ans = new StackPanel{};
		Content = ans;
		{{
			var textBlock = new SelectableTextBlock{
				Text = "https://github.com/Tsinswreng/csngaq"
			};
			ans.Children.Add(textBlock);
		}}//ans:stackPanel
		return 0;
	}
}