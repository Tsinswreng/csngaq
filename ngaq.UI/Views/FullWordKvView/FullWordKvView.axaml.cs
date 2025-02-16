using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using Avalonia.Interactivity;
using ngaq.UI.ViewModels.FullWordKv;
using ngaq.UI.ViewModels.KV;
using ngaq.UI.Views.KV;


namespace ngaq.UI.Views.FullWordKvView;

public partial class FullWordKvView : UserControl{
	public FullWordKvView(){
		InitializeComponent();
		this.DataContext = new FullWordKvVm();
		var z = this;
		if(!(DataContext is FullWordKvVm)){
			return;
		}
		var root = z.FindControl<UserControl>("Root");

		z._fn_renderList(
			()=>ctx!.propertyVms
			,nameof(ctx.propertyVms)
			,"propertys"
		)();

		z._fn_renderList(
			()=>ctx!.learnVms
			,nameof(ctx.learnVms)
			,"learns"
		)();
	}

	public FullWordKvVm? ctx{
		get{return DataContext as FullWordKvVm;}
	}



	protected Func<zero> _fn_renderList(
		//ObservableCollection<KvVm> vms
		Func<ObservableCollection<KvVm>> getVmsFn
		,str bindingName
		,string titleText
	){
		var z = this;
		return ()=>{
			var grid = new Grid(){
				RowDefinitions= [
					new RowDefinition{Height=GridLength.Auto}
					,new RowDefinition{Height=GridLength.Auto}
					,new RowDefinition{Height=new GridLength(1, GridUnitType.Star)}
				]
			};
			{//grid:Grid
				var title = new TextBlock(){
					Text = titleText
				};
				title.Classes.Add("Cls_ColTitle");
				Grid.SetRow(title, 0);
				grid.Children.Add(title);
				//
				var separator = new Separator();
				Grid.SetRow(separator, 1);
				grid.Children.Add(separator);
				//
				var scrollViewer = new ScrollViewer();
				Grid.SetRow(scrollViewer, 2);
				grid.Children.Add(scrollViewer);

				var index = 0;
				var itemsControl = new ItemsControl{
					ItemsSource = getVmsFn(),
					ItemTemplate = new FuncDataTemplate<KvVm>((vm, _) =>{
						if(index >= getVmsFn().Count){
							index = 0;
						}
						var stackPanel = new StackPanel();
						{//stackPanel:StackPanel
							var indexBlock = new TextBlock{
								Text=index.ToString()
							};
							stackPanel.Children.Add(indexBlock);
							//
							var kvView = new KvView { DataContext = vm };
							stackPanel.Children.Add(kvView);
							//
							stackPanel.Children.Add(
								new Separator()
							);
						}//~stackPanel:StackPanel
						index++;
						return stackPanel;
					})
				};
				itemsControl.Bind(
					ItemsControl.ItemsSourceProperty
					,new Binding(bindingName){Mode = BindingMode.TwoWay}
				);
				scrollViewer.Content = itemsControl;
			}//~grid:Grid


			if (Content is Panel panel){
				panel.Children.Add(grid);
			}
			return 0;
		};
	}

}
