using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using Microsoft.Extensions.DependencyInjection;
using ngaq.Core.model.Sample;
using ngaq.UI.viewModels.FullWordKv;
using ngaq.UI.viewModels.WordCrud;
using ngaq.UI.views.wordInfo;



namespace ngaq.UI.views.WordCrud;
[Obsolete]
public partial class WordCrudView : UserControl{
	public WordCrudView(){
		//InitializeComponent();
		//this.DataContext = new WordCrudVm();
		this.DataContext = App.ServiceProvider.GetRequiredService<WordCrudVm>();
		_render();
	}

	public WordCrudVm? ctx{
		get{return DataContext as WordCrudVm;}
	}

	protected zero _render(){
		var z = this;
		//var ctx = z.DataContext as WordCrudVm;
		if(ctx == null){
			return 0;
		}
		// var root = z.FindControl<UserControl>("Root");
		// if(root == null){
		// 	throw new System.Exception("Root control not found.");
		// }
		var stackPanelHori = new StackPanel(){
			Orientation=Orientation.Horizontal
		};
		{{//stackPanelHori:StackPanel
			var stackPanelVert = new StackPanel(){
				Orientation=Orientation.Vertical
			};
			{{//stackPanelVert:StackPanel

				var titleBox = new TextBlock(){
					Text="由id尋FullWord"
				};
				stackPanelVert.Children.Add(titleBox);
				//
				var searchBox = new TextBox();
				searchBox.Bind(
					TextBox.TextProperty
					,new Binding(nameof(ctx.searchId)) { Mode = BindingMode.TwoWay }
				);
				stackPanelVert.Children.Add(searchBox);
				//
				var searchButton = new Button(){
					Content="尋"
				};
				searchButton.Click += (sender, e) => {
					ctx.seekFullWordKvByIdAsync().ContinueWith(d=>{});
				};
				stackPanelVert.Children.Add(searchButton);
				//
				var testFillButton = new Button(){
					Content="試填"
				};
				testFillButton.Click += (sender, e) => {
					var fullWord = FullWordSample.getInst().sample;
					ctx.fromModel(fullWord);
				};
				stackPanelVert.Children.Add(testFillButton);
				//
				var clearButton = new Button(){
					Content="清除"
				};
				clearButton.Click += (sender, e) => {//t
					//ctx.fullWordKvVm = new FullWordKvVm();
					var fenestra = new Window(){
						Width=1920/4
						,Height=1080/4
						,Title="title"
					};
					fenestra.Show();
				};
				stackPanelVert.Children.Add(clearButton);
				//
				var printButton = new Button(){
					Content="print"
				};
				printButton.Click += (sender, e) => {
					G.log("print");
					foreach(var kv in ctx.fullWordKvVm.propertyVms){
						G.logJson(kv.toModel());
					}
				};
				stackPanelVert.Children.Add(printButton);


			}}//~stackPanelVert:StackPanel
			stackPanelHori.Children.Add(stackPanelVert);
			//
			var wordInfoView = new WordInfo(){
				Width=600
			};
			wordInfoView.Bind(
				Control.DataContextProperty
				,new Binding(nameof(ctx.wordInfoVm)) { Mode = BindingMode.TwoWay }
			);
			stackPanelHori.Children.Add(wordInfoView);
			//
			var fullWordKvView = new FullWordKvView.FullWordKvView(){};
			fullWordKvView.Bind(
				Control.DataContextProperty
				,new Binding(nameof(ctx.fullWordKvVm)) { Mode = BindingMode.TwoWay }
			);
			stackPanelHori.Children.Add(fullWordKvView);
			//
		}}//~stackPanel:StackPanel
		this.Content = stackPanelHori;
		return 0;
	}
}
