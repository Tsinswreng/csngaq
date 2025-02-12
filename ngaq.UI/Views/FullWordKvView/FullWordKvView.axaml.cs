using Avalonia.Controls;
using Avalonia.Interactivity;
using ngaq.UI.ViewModels.FullWordKv;
using ngaq.UI.ViewModels.KV;

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
