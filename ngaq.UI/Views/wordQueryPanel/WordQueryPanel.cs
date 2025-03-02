using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Media;
using ngaq.UI.viewModels.wordQueryPanel;
using ngaq.UI.views.kv;
using ngaq.UI.views.wordInfo;

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
				// new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)}
				// ,new ColumnDefinition{Width = new GridLength(2, GridUnitType.Star)}
				new ColumnDefinition{Width = new GridLength(0x120)}
				//,new ColumnDefinition{Width = GridLength.Auto}
				,new ColumnDefinition{Width = new GridLength(0x4)}
				,new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)}
			]);
		}
		{{
			var leftSearchBar = _left();
			ans.Children.Add(leftSearchBar);
			{
				Grid.SetColumn(leftSearchBar, 0);
			}

			var gsplit = new GridSplitter{};
			ans.Children.Add(gsplit);
			{
				var o = gsplit;
				Grid.SetColumn(o, 1);
				o.ResizeDirection = GridResizeDirection.Columns;
				//o.Background = Brushes.SkyBlue;
				o.BorderBrush = Brushes.Aqua;
				o.BorderThickness = new Thickness(1, 0, 0, 0);
			}

			var right = _right();
			ans.Children.Add(right);
			{
				Grid.SetColumn(right, 2);
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


	protected Control _right(){


		var tab = new TabControl{};
		{{
			var head_kv = new TabItem{Header = "Key-Value"};
			tab.Items.Add(head_kv);
			{{
				var kvView = _kvView();
				head_kv.Content = kvView;
			}}

			var wordInfo = new TabItem{Header = "Word Info"};
			tab.Items.Add(wordInfo);
			{{
				var wordInfoView = _wordInfo();
				wordInfo.Content = wordInfoView;
			}}
		}}//~tab
		return tab;
	}



	protected Control _kvView(){
		var scrl = new ScrollViewer{};
		{
			var o = scrl;
			Grid.SetColumn(o, 2);
			o.Classes.Add(cls.Stretch);
		}
		{{
			var kvView = new KvView();
			scrl.Content = kvView;
			{
				var o = kvView;
				Grid.SetColumn(o, 1);
				o.Bind(
					DataContextProperty
					,new Binding(nameof(ctx.kvVm)){Mode=BindingMode.TwoWay}
				);
			}
		}}
		return scrl;
	}

	protected Control _wordInfo(){
		var ans = new WordInfo{};
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
				ctx?.showAtKvView(vm.wordKv);
			};

			return ans;
		});
		return ans;
	}




}