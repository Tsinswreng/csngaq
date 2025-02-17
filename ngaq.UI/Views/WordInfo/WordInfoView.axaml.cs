using Avalonia.Controls;
using Avalonia.Data;
using ngaq.UI.ViewModels.WordInfo;

namespace ngaq.UI.Views.WordInfo;
public partial class WordInfoView: UserControl {
	public WordInfoView() {
		DataContext = new WordInfoVm();
		_render();
	}


	public WordInfoVm? ctx{
		get{return DataContext as WordInfoVm;}
	}

	protected zero _render(){
		var z = this;
		var vert = new StackPanel();
		{//vert:StackPanel
			//
			var wordTextTitle = new TextBlock();
			wordTextTitle.Bind(
				TextBlock.TextProperty
				,new Binding(
					nameof(ctx.fullWordKvVm.textWordVm.kStr)
					,BindingMode.TwoWay
				)
			);
			vert.Children.Add(wordTextTitle);
		}//~vert:StackPanel
		z.Content = vert;



		return 0;
	}



}