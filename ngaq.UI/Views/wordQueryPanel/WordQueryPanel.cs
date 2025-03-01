using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using ngaq.UI.viewModels.wordQueryPanel;
using ngaq.UI.views.kv;

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
		// Content = _left();
		// var t = new AutoCompleteBox{};
		// t.Classes.Add(cls.Stretch);
		// Content = t;

		//return 0; //t
		var ans = new Grid{};
		Content = ans;
		{
			var o = ans;
			o.ColumnDefinitions.AddRange([
				new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)}
				,new ColumnDefinition{Width = new GridLength(2, GridUnitType.Star)}
			]);
		}
		{{
			var leftSearchBar = _left();
			ans.Children.Add(leftSearchBar);
			{
				Grid.SetColumn(leftSearchBar, 0);
			}

			var kvView = new KvView();
			ans.Children.Add(kvView);
			{
				var o = kvView;
				Grid.SetColumn(o, 1);
				o.Bind(
					DataContextProperty
					,new Binding(nameof(ctx.kvVm)){Mode=BindingMode.TwoWay}
				);
			}

		}}//~ans
		return 0;
	}

	protected Control _left(){
		var ans = new StackPanel{
			Orientation = Avalonia.Layout.Orientation.Vertical
		};
		{
			var o = ans;
			o.Classes.Add(cls.Stretch);
		}
		{{
			var searchBox = new AutoCompleteBox{};
			ans.Children.Add(searchBox);
			{
				var o = searchBox;
				o.Watermark = "Search by word text or id";
				o.Classes.Add(cls.Stretch);
				o.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Stretch;
				// o.Bind(
				// 	WidthProperty
				// 	,new Binding(nameof(WrapPanel.Width)){
				// 		RelativeSource = new RelativeSource(){
				// 			AncestorType = typeof(WrapPanel)
				// 		}
				// 	}
				// );
			}

			var searchButton = new Button{};
			ans.Children.Add(searchButton);
			{
				var o = searchButton;
				o.Content = "Search";
				//o.HorizontalAlignment=Avalonia.Layout.HorizontalAlignment.Stretch;
				o.Classes.Add(cls.Stretch);
				o.Click += (sender, e) => {
					ctx?.click_searchBtn();
				};
			}

			var line = new Separator{};
			ans.Children.Add(line);

			var results = _results();
			ans.Children.Add(results);
		}}//~ans
		return ans;
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