using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml.Templates;
using ngaq.UI.viewModels.wordQueryPanel;

namespace ngaq.UI.views.wordQueryPanel;

public partial class WordQueryPanel
	:UserControl
{

	public WordQueryPanel(){
		ctx = new WordQueryPanelVm();
		_style();
		_render();
	}

	public WordQueryPanelVm? ctx{
		get{return DataContext as WordQueryPanelVm;}
		set{DataContext = value;}
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
				o.Click += (sender, e) => {
					ctx?.click_searchBtn();
				};
			}

			var line = new Separator{};
			ans.Children.Add(line);

			var results = _results();
			ans.Children.Add(results);
		}}//~ans
		return 0;
	}



	protected Control _results(){
		var ans = new ItemsControl{};
		{
			var o = ans;
			o.Bind(
				ItemsControl.ItemsSourceProperty
				,new Binding(nameof(ctx.searchedWords))
			);
		}
		ans.ItemTemplate = new FuncDataTemplate<SearchedWordCardVm>((vm, _) => {
			var ans = new SearchedWordCard();
			ans.Bind(
				SearchedWordCard.DataContextProperty
				,new Binding()
			);
			ans.click = (sender, e) => {
				G.log(vm.id);//t
			};

			return ans;
		});
		return ans;
	}




}