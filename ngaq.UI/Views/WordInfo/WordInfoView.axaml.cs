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
		return 0;
	}



	protected zero _render(){
		var z = this;
		if(ctx == null){
			return 0;
		}
		ctx.fullWordKvVm.fromModel(FullWordSample.getInst().sample);//TODO for test



		var vert = new StackPanel();
		{//vert:StackPanel
			//
			var N_fullWordKvVm = nameof(ctx.fullWordKvVm);
			var N_textWordVm = nameof(ctx.fullWordKvVm.textWordVm);
			var N_kStr = nameof(ctx.fullWordKvVm.textWordVm.kStr);
			var N_id = nameof(ctx.fullWordKvVm.textWordVm.id);
			var N_bl = nameof(ctx.fullWordKvVm.textWordVm.bl);
			//
			var idEtLangPanel = new StackPanel(){
				Orientation=Orientation.Horizontal
			};
			{//idEtLangPanel:StackPanel
				var idBlock = new TextBlock(){};
				idBlock.Bind(
					TextBlock.TextProperty
					,new Binding(
						N_fullWordKvVm+"."+N_textWordVm+"."+N_id
					)
				);
				idEtLangPanel.Children.Add(idBlock);
				//
				var langBlock = new TextBlock(){};
				langBlock.Bind(
					TextBlock.TextProperty
					,new Binding(
						N_fullWordKvVm+"."+N_textWordVm+"."+N_bl
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
					N_fullWordKvVm+"."+N_textWordVm+"."+N_kStr
					,BindingMode.TwoWay
				)
			);
			vert.Children.Add(wordTextTitle);
			//
			var sep = new Separator();
			vert.Children.Add(sep);
			//
			var meansScrollViewer = new ScrollViewer();
			{//meansScrollViewer:ScrollViewer
				var meansItemsControl = new ItemsControl(){
					// ItemsSource = ctx.fullWordKvVm.propertyVms.Where(e=>
					// 	e.bl==BlPrefix.join(BlPrefix.Property, PropertyEnum.mean.ToString())
					// )
					ItemsSource=ctx.fullWordKvVm.propertyVms
					,ItemTemplate = new FuncDataTemplate<KvVm>((vm, _)=>{
						if(vm.bl!=BlPrefix.join(BlPrefix.Property, PropertyEnum.mean.ToString())){
							return null;
						}
						var oneMeanStackPanel = new StackPanel();
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
							}//~idPanelBorder
							oneMeanStackPanel.Children.Add(idPanelBorder);
							//
							var oneMeanTextBlock = new TextBlock(){
								Text = vm.vStr
							};
							oneMeanStackPanel.Children.Add(oneMeanTextBlock);
							//
							var sep = new Separator();
							oneMeanStackPanel.Children.Add(sep);
							//
							return oneMeanStackPanel;
						}//~oneMeanStackPanel:StackPanel
					})
				};
				meansScrollViewer.Content = meansItemsControl;
			}//~meansScrollViewer:ScrollViewer
			vert.Children.Add(meansScrollViewer);


		}//~vert:StackPanel
		z.Content = vert;
		return 0;
	}
}

