using System.Collections;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using ngaq.Core.Model;
using ngaq.Core.Model.consts;
using ngaq.Core.Model.sample;
using ngaq.model.consts;
using ngaq.UI.ViewModels.KV;
using ngaq.UI.ViewModels.WordInfo;

namespace ngaq.UI.Views.WordInfo;




public partial class WordInfoView: UserControl {
	public WordInfoView() {
		DataContext = new WordInfoVm();
		_render();
		_style();
	}


	public WordInfoVm? ctx{
		get{return DataContext as WordInfoVm;}
	}

	//css類名
	public enum Cls{
		WordText
		,MeanBox
	}


	protected zero _style(){
		var sty_WordText = new Style(x=>x
			.Is<Control>()
			.Class(nameof(Cls.WordText))
		);
		sty_WordText.Setters.Add(new Setter(
			TextBlock.FontSizeProperty
			,32.0
		));
		Styles.Add(sty_WordText);
		//
		var sty_separator = new Style(
			x=>x
			.Is<Separator>()
		);
		sty_separator.Setters.Add(new Setter(
			Separator.MarginProperty
			,new Thickness(0)
		));
		Styles.Add(sty_separator);
		//
		var sty_MeanBox = new Style(x=>x
			.Is<Border>()
			.Class(nameof(Cls.MeanBox))
		);
		sty_MeanBox.Setters.Add(new Setter(
			Border.MaxHeightProperty
			,60.0
		));
		Styles.Add(sty_MeanBox);
		return 0;
	}



	protected zero _render(){
		var z = this;
		if(ctx == null){
			return 0;
		}
		ctx.fromModel(FullWordSample.getInst().sample);//TODO for test
		var vert = new StackPanel();
		{//vert:StackPanel
			//
			var N_fullWordKv = nameof(ctx.fullWordKv);
			var N_textWord = nameof(ctx.fullWordKv.textWord);
			var N_kStr = nameof(ctx.fullWordKv.textWord.kStr);
			var N_id = nameof(ctx.fullWordKv.textWord.id);
			var N_bl = nameof(ctx.fullWordKv.textWord.bl);

			//
			var idEtLangPanel = new StackPanel(){
				Orientation=Orientation.Horizontal
			};
			{//idEtLangPanel:StackPanel
				var idBlock = new TextBlock(){};
				idBlock.Bind(
					TextBlock.TextProperty
					,new Binding(
						N_fullWordKv+"."+N_textWord+"."+N_id
					)
				);
				idEtLangPanel.Children.Add(idBlock);
				//
				var langBlock = new TextBlock(){};
				langBlock.Bind(
					TextBlock.TextProperty
					,new Binding(
						//N_fullWordKv+"."+N_textWord+"."+N_bl
						nameof(ctx.lang)
					)
				);
				idEtLangPanel.Children.Add(langBlock);
			}//~idEtLangPanel:StackPanel
			vert.Children.Add(idEtLangPanel);
			//
			vert.Children.Add(new Separator());
			//
			var wordTextTitle = new TextBlock();
			wordTextTitle.Classes.Add(nameof(Cls.WordText));
			wordTextTitle.Bind(
				TextBlock.TextProperty
				,new Binding(
					N_fullWordKv+"."+N_textWord+"."+N_kStr
					,BindingMode.TwoWay
				)
			);
			vert.Children.Add(wordTextTitle);
			//
			var sep = new Separator();
			vert.Children.Add(sep);
			//
			var otherPropsSclViewer = new ScrollViewer();
			{//otherPropsSclViewer
				var othersPropItemsControl = new ItemsControl(){
					ItemsSource = ctx.otherProps
					,ItemTemplate = new FuncDataTemplate<I_PropertyKv>((propKv, _)=>{
						var hori = new StackPanel(){
							Orientation = Orientation.Horizontal
						};
						{//hori:StackPanel
							var propName = BlPrefix.split(propKv.bl??"").Item2;
							//
							var label = new TextBlock(){
								Text = propName+": "
							};
							hori.Children.Add(label);
							//
							var propV = propKv.vStr;
							var propVBlock = new TextBlock(){
								Text = propV
							};
							hori.Children.Add(propVBlock);
						}//~hori:StackPanel
						return hori;
					})
				};
				otherPropsSclViewer.Content = othersPropItemsControl;
			}//~otherPropsSclViewer
			vert.Children.Add(otherPropsSclViewer);

			var meansScrollViewer = new ScrollViewer();
			{//meansScrollViewer:ScrollViewer
				var meansItemsControl = new ItemsControl(){
					ItemsSource=ctx.means
					,ItemTemplate = new FuncDataTemplate<I_PropertyKv>((vm, _)=>{
						return _means(vm);
					})//~ItemTemplateFn
				};//~meansItemsControl.ctor
				meansScrollViewer.Content = meansItemsControl;
			}//~meansScrollViewer:ScrollViewer
			vert.Children.Add(meansScrollViewer);
		}//~vert:StackPanel
		z.Content = vert;
		return 0;
	}

	protected Control? _means(I_PropertyKv vm){
		if(vm.bl!=BlPrefix.join(BlPrefix.Property, PropertyEnum.mean.ToString())){
			return null;
		}
		var ansBorder = new Border(){
			// BorderBrush = Brushes.Yellow
			// ,BorderThickness = new Thickness(1)
		};
		//ansBorder.Classes.Add(nameof(Cls.MeanBox));
		{//ansBorder:Border
			var oneMeanStackPanel = new StackPanel();
			//oneMeanStackPanel.Classes.Add(nameof(Cls.MeanBox));
			{//oneMeanStackPanel:StackPanel
				var idPanelBorder = new Border(){
					BorderBrush = Brushes.Gray
					,BorderThickness = new Thickness(1)
				};
				{//idPanelBorder
					var idPanel = new StackPanel(){
						Orientation=Orientation.Horizontal
					};
					{//idPanel:StackPanel
						var idLabel = new TextBlock(){
							Text = "id: "
						};
						idPanel.Children.Add(idLabel);
						//
						var idBlock = new TextBlock(){
							Text = vm.id.ToString()
						};
						idPanel.Children.Add(idBlock);
					}//~idPanel:StackPanel
					idPanelBorder.Child = idPanel;
					oneMeanStackPanel.Children.Add(idPanelBorder);
				}//~idPanelBorder
				//
				var border2 = new Border(){
					MaxHeight = 40.0
					,BorderBrush = Brushes.Yellow
				};

				{//border2:Border
					//
					var oneMeanScrlVwr = new ScrollViewer();
					{//oneMeanScrlVwr:ScrollViewer
						var oneMeanContentStackPanel = new StackPanel();
						{//oneMeanContentStackPanel:StackPanel
							//
							var oneMeanTextBlock = new TextBlock(){
								Text = vm.vStr
							};
							oneMeanStackPanel.Children.Add(oneMeanTextBlock);
						//
							var sep = new Separator();
							oneMeanStackPanel.Children.Add(sep);
						}//~oneMeanContentStackPanel:StackPanel
						oneMeanScrlVwr.Content = oneMeanContentStackPanel;
					}//~oneMeanScrlVwr:ScrollViewer
					border2.Child = oneMeanScrlVwr;
				}//~border2:Border
				oneMeanStackPanel.Children.Add(border2);
			}//~oneMeanStackPanel:StackPanel
			ansBorder.Child = oneMeanStackPanel;
		}//~ansBorder:Border
		return ansBorder;
		//

	}
}

