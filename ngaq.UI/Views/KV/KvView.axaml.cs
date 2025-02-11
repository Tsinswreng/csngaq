using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ngaq.Core.model;
using ngaq.Core.model.sample;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.Views.KV;

public partial class KvView : UserControl{

	public static readonly StyledProperty<I_WordKv> WordKvProperty
		= AvaloniaProperty.Register<KvView, I_WordKv>(nameof(WordKv));


	public I_WordKv WordKv{
		get{return GetValue(WordKvProperty);}
		set{
			G.log(value);
			SetValue(WordKvProperty, value);
			if( value is I_WordKv && DataContext is KvVM ctx){
				ctx.fromModel((I_WordKv)value);
			}
		}
	}


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
