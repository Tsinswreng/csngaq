using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ngaq.Core.model.sample;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.Views.KV;

public partial class KvView : UserControl{
	public KvView(){
		InitializeComponent();
		this.DataContext = new KvVM();
	}


	private void InitWord_Click(object sender, RoutedEventArgs e){
		if(!( this.DataContext is KvVM )){
			G.log("!( dataCtx is KvVM )");
			G.log(this.DataContext);
			return;
		}
		var dataCtx = (KvVM)this.DataContext;
		dataCtx.fromModel(FullWordSample.getInst().sample.textWord);
	}
	

	private void printChangedModel_Click(object sender, RoutedEventArgs e){
		if(!( this.DataContext is KvVM )){return;}
		var dataCtx = (KvVM)this.DataContext;
		var ans = dataCtx.toModel();
		G.logJson(ans);
	}
}
