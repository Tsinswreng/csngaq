using Avalonia.Controls;
using Avalonia.Interactivity;
using ngaq.UI.ViewModels.FullWordKv;
using ngaq.UI.ViewModels.KV;
using ngaq.UI.Views.KV;


namespace ngaq.UI.Views.FullWordKvView;

public partial class FullWordKvView : UserControl{
	public FullWordKvView(){
		InitializeComponent();
		this.DataContext = new FullWordKvVm();
		// if(DataContext is FullWordKvVm vm
		// 	&& textWord.DataContext is KvVm kvVm
		// ){
		// 	kvVm.fromModel(vm.textWord);
		// }
		//Content = "123";
		var z = this;
		if(!(DataContext is FullWordKvVm)){
			return;
		}
		var ctx = (FullWordKvVm)DataContext!;
		var root = z.FindControl<UserControl>("Root");{

			var grid = new Grid(){
				RowDefinitions= [
					new RowDefinition{Height=GridLength.Auto}
					,new RowDefinition{Height=GridLength.Auto}
					,new RowDefinition{Height=new GridLength(1, GridUnitType.Star)}
				]
			};
			{//grid:Grid
				var title = new TextBlock(){
					Text = "propertys"
				};
				title.Classes.Add("Cls_ColTitle");
				Grid.SetRow(title, 0);
				grid.Children.Add(title);

				var separator = new Separator();
				Grid.SetRow(separator, 1);
				grid.Children.Add(separator);

				var scrollViewer = new ScrollViewer();
				Grid.SetRow(scrollViewer, 2);
				grid.Children.Add(scrollViewer);
				var container = new StackPanel();
				scrollViewer.Content = container;
				{//container:StackPanel
					var len = ctx.propertyVms.Count;
					for(var i = 0; i < len; i++){
						var curVm = ctx.propertyVms[i];
						var stackPanel = new StackPanel();
						{//stackPanel:StackPanel
							var indexBlock = new TextBlock{
								Text=i.ToString()
							};
							stackPanel.Children.Add(indexBlock);
							var kvView = new KvView(){
								DataContext = curVm
							};
							stackPanel.Children.Add(kvView);

							if(i != len - 1){
								stackPanel.Children.Add(
									new Separator()
								);
							}
						}//~stackPanel:StackPanel
						container.Children.Add(stackPanel);
					}

				}//~container:StackPanel

				if(Content is Panel p){//Panel之子類纔能用.Children.Add
					p.Children.Add(grid);
				}
			}//~grid:Grid
		}
	}


	private void printChangedModel_Click(object sender, RoutedEventArgs e){
		if(!( this.DataContext is FullWordKvVm )){return;}
		var dataCtx = (FullWordKvVm)this.DataContext;
		//G.logJson(dataCtx.textWord);
	}

	private void printChangedModel_Click2(object sender, RoutedEventArgs e){
		if(!( this.DataContext is FullWordKvVm )){return;}
		var dataCtx = (FullWordKvVm)this.DataContext;
		G.logJson(dataCtx.textWordVm.toModel());
	}


}
