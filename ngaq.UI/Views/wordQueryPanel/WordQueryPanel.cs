using Avalonia.Controls;

namespace ngaq.UI.views.wordQueryPanel;

public partial class WordQueryPanel
	:UserControl
{

	public WordQueryPanel(){
		_render();
	}

	protected zero _render(){
		var ans = new WrapPanel{
			Orientation = Avalonia.Layout.Orientation.Vertical
		};
		Content = ans;
		{{
			var searchBox = new AutoCompleteBox{};
			ans.Children.Add(searchBox);
			{
				var o = searchBox;
				o.Watermark = "Search by word text or id";
			}

			var searchButton = new Button{};
			ans.Children.Add(searchButton);
			{
				var o = searchButton;
				o.Content = "Search";
				o.HorizontalAlignment=Avalonia.Layout.HorizontalAlignment.Stretch;
			}
		}}//~ans
		return 0;
	}


	

}