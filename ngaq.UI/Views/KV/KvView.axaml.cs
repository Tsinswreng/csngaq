using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using ngaq.Core.Model;
using ngaq.Core.Model.sample;
using ngaq.UI.ViewModels.KV;

namespace ngaq.UI.Views.KV;

public partial class KvView : UserControl{

	public static readonly StyledProperty<I_WordKv> WordKvProperty
		= AvaloniaProperty.Register<KvView, I_WordKv>(nameof(WordKv));
	/// <summary>
	/// WordKv="{Binding xxx}"旹、斯屬性ʹ訪問器與修改器不會被觸發。
	/// 改用<MyControl DataContext="{Binding WordKv}">
	/// </summary>
	public I_WordKv WordKv{
		get{
			G.log("get");
			return GetValue(WordKvProperty);
		}set{
			G.log(123);
			G.log(value);
			SetValue(WordKvProperty, value);
			if( value is I_WordKv && DataContext is KvVm ctx){
				ctx.fromModel((I_WordKv)value);
			}
		}
	}


	public KvView(){
		InitializeComponent();
		this.DataContext = new KvVm();
	}


	private void InitWord_Click(object sender, RoutedEventArgs e){
		if(!( this.DataContext is KvVm )){
			G.log("!( dataCtx is KvVM )");
			G.log(this.DataContext);
			return;
		}
		var dataCtx = (KvVm)this.DataContext;
		dataCtx.fromModel(FullWordSample.getInst().sample.textWord);
	}


	private void printChangedModel_Click(object sender, RoutedEventArgs e){
		if(!( this.DataContext is KvVm )){return;}
		var dataCtx = (KvVm)this.DataContext;
		var ans = dataCtx.toModel();
		G.logJson(ans);
		G.log(this.DataContext);
	}
}
