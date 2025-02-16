using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Layout;
using ngaq.Core.model.sample;
using ngaq.UI.ViewModels.WordCrud;

namespace ngaq.UI.Views.WordCrud;

public partial class WordCrudView : UserControl{
	public WordCrudView(){
		InitializeComponent();
		this.DataContext = new WordCrudVm();
		_render();
	}

	protected zero _render(){
		var z = this;
		var ctx = z.DataContext as WordCrudVm;
		if(ctx == null){
			return 0;
		}
		var root = z.FindControl<UserControl>("Root");
		if(root == null){
			throw new System.Exception("Root control not found.");
		}
		var stackPanelHori = new StackPanel(){
			Orientation=Orientation.Horizontal
		};
		{{//stackPanel:StackPanel
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
					Content="尋找"
				};
				searchButton.Click += (sender, e) => {
					ctx.fullWordKvVm?.fromModel(
						FullWordSample.getInst().sample
					);
				};
				stackPanelVert.Children.Add(searchButton);
				
			}}//~stackPanelVert:StackPanel
			stackPanelHori.Children.Add(stackPanelVert);
			//
			var fullWordKvView = new FullWordKvView.FullWordKvView(){};
			fullWordKvView.Bind(
				Control.DataContextProperty
				,new Binding(nameof(ctx.fullWordKvVm)) { Mode = BindingMode.TwoWay }
			);
			stackPanelHori.Children.Add(fullWordKvView);

		}}//~stackPanel:StackPanel
		root.Content = stackPanelHori;
		return 0;
	}
}
