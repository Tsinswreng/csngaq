using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using ngaq.UI.viewModels.wordQueryPanel;
using Shr.Avalonia;
using Shr.Avalonia.ext;
//using TextBlock = Avalonia.Controls.SelectableTextBlock;
using Ctx = ngaq.UI.viewModels.wordQueryPanel.SearchedWordCardVm;
namespace ngaq.UI.views.wordQueryPanel;

public partial class SearchedWordCard
	:UserControl
{

	public SearchedWordCard(){
		//debug();
		useSample();
		_style();
		_render();
		var z = this;
	}

	public SearchedWordCardVm? ctx{
		get{return DataContext as SearchedWordCardVm;}
		set{DataContext = value;}
	}


	public zero useSample(){
		ctx = SearchedWordCardVm.sample;
		return 0;
	}


	public zero debug(){
		var showGridLine = new Style(x=>
			x.Is<Grid>()
		);
		Styles.Add(showGridLine);
		{
			showGridLine.set(
				Grid.ShowGridLinesProperty
				,true
			);
		}
		return 0;
	}


	public Action<object?, EventArgs>? click{get;set;}


	protected Control _render(){

		var btn = new Button{
			// BorderThickness = new Thickness(1)
			// ,BorderBrush = Brushes.Yellow
		};
		Content = btn;
		{
			var o = btn;
			o.Height = 42;
			o.Classes.Add(cls.Container);
			//o.HorizontalAlignment=Avalonia.Layout.HorizontalAlignment.Stretch;
			o.Click += (a,b)=>{
				click?.Invoke(a,b);
			};
		}
		{{
			var grid = new Grid();
			btn.Content = grid;
			{
				var o = grid;
				o.RowDefinitions.AddRange([
					new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) }
					,new RowDefinition{ Height = new GridLength(2, GridUnitType.Star) }
					//,new RowDefinition{ Height = new GridLength(1, GridUnitType.Star) }
				]);
			}
			{{
				// var props = new TextBlock{Text="123"};
				// grid.Children.Add(props);
				//var row0 = new Grid();
				var row0 = new WrapPanel();
				grid.Children.Add(row0);
				{
					// row0.ColumnDefinitions.AddRange([
					// 	new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
					// 	,new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
					// 	,new ColumnDefinition{ Width = new GridLength(1, GridUnitType.Star) }
					// ]);
					row0.Orientation = Avalonia.Layout.Orientation.Horizontal;
				}
				{{
					var id = new TextBlock{};
					row0.Children.Add(id);
					{
						id.Classes.Add(cls.SubText);
						id.Bind(
							TextBlock.TextProperty
							,new CBE(CBE.pth<Ctx>(x=>x.id))
						);
					}

					row0.Children.Add(
						new TextBlock{Text="	"}
					);

					var bl = new TextBlock{};
					row0.Children.Add(bl);
					{
						//Grid.SetColumn(bl, 1);
						bl.Classes.Add(cls.SubText);
						bl.Bind(
							TextBlock.TextProperty
							,new CBE(CBE.pth<Ctx>(x=>x.bl))
						);
					}

					row0.Children.Add(
						new TextBlock{Text="	"}
					);

					var fKeyPanel = new StackPanel{};
					row0.Children.Add(fKeyPanel);
					{
						var o = fKeyPanel;
						Grid.SetColumn(o, 2);
						o.Orientation = Avalonia.Layout.Orientation.Horizontal;
						o.Bind(
							IsVisibleProperty
							,new CBE(CBE.pth<Ctx>(x=>x.fKey)){
								Converter = new FuncValueConverter<u64?, bool>(x=>x != null)
							}
						);
					}
					{{

						var fKeyLabel = new TextBlock{Text="外鍵:"};
						fKeyPanel.Children.Add(fKeyLabel);

						var fKey = new TextBlock{};
						fKeyPanel.Children.Add(fKey);
						{
							fKey.Classes.Add(cls.SubText);
							fKey.Bind(
								TextBlock.TextProperty
								,new CBE(CBE.pth<Ctx>(x=>x.fKey))
							);
						}
						//
					}}

				}}//~row0:Grid

				var text = new TextBlock{};
				grid.Children.Add(text);
				{
					Grid.SetRow(text, 1);
					text.Classes.Add(cls.MainText);
					text.Bind(
						TextBlock.TextProperty
						,new CBE(CBE.pth<Ctx>(x=>x.text))
					);
				}
			}}//~ans:Grid
		}}//~border:Border
		return btn;
	}
}

/*
avalonia
grid有兩行、第一行是"123"、第二行是"peer"
顯示的效果是: 第一行的"123" 字的底部有一部分沒顯示出來 被黑色遮住了
但是第二行的"peer"上面還明明有一段空間。
能不能讓字不要被遮住、讓他完整地顯示出來、即使高度不夠 寧可讓 超出的部分顯示到下面去 也不要被遮住?
 */