using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Controls.Primitives;
using Avalonia.Data;
using Avalonia.Media;
using Avalonia.Styling;
using ngaq.UI.viewModels.wordQueryPanel;
using Shr.Avalonia.ext;
using Shr.Avalonia.util;
using TextBlock = Avalonia.Controls.SelectableTextBlock;
namespace ngaq.UI.views.wordQueryPanel;
public partial class SearchedWordCard{
	protected zero _style(){
		//似不效
		var noMarginPadding = new Style(x=>
			x.Is<Control>()
		);
		Styles.Add(noMarginPadding);
		{
			noMarginPadding.set(
				TemplatedControl.MarginProperty
				,new Thickness(0)
			);
			noMarginPadding.set(
				TemplatedControl.PaddingProperty
				,new Thickness(0)
			);
		}

		var mainText = new Style(x=>
			x.Is<Control>()
			.Class(cls.MainText)
		);
		Styles.Add(mainText);
		{
			mainText.set(
				FontSizeProperty
				,20.0
			);
		}

		var subText = new Style(x=>
			x.Is<Control>()
			.Class(cls.SubText)
		);
		Styles.Add(subText);
		{
			subText.set(
				ForegroundProperty
				,new SolidColorBrush(Colors.LightGray)
			);
		}

		// var container = new Style(x=>
		// 	x.Is<Control>()
		// 	.Class(cls.Container)
		// );
		// Styles.Add(container);
		// {
		// 	// container.set(
		// 	// 	BackgroundProperty
		// 	// 	,new SolidColorBrush(Colors.White)
		// 	// );//worked
		// }

		var containerPtrOver = new Style(x=>
			x.Is<Control>()
			.Class(cls.Container)
			.Class(PsdCls.inst.pointerover)
			.Template()
			.OfType<ContentPresenter>()
		);
		Styles.Add(containerPtrOver);
		{
			var o = containerPtrOver;
			o.set(
				BorderBrushProperty
				,Brushes.Aqua
			);
			o.set(
				BorderThicknessProperty
				,new Thickness(1)
			);
			// o.set(
			// 	BackgroundProperty
			// 	,new SolidColorBrush(Colors.White)
			// );
		}

		// var containerPressed = new Style(x=>
		// 	x.Is<Control>()
		// 	.Class(cls.Container)
		// 	.Class(":pressed")
		// 	//.Template()
		// );
		// Styles.Add(containerPressed);
		// {
		// 	var o = containerPressed;
		// 	o.set(
		// 		BackgroundProperty
		// 		,new SolidColorBrush(Colors.LightBlue)
		// 	);
		// }

		var testPressed = new Style(x=>
			x.Is<Control>()
			.Class(":pressed")
		);
		Styles.Add(testPressed);
		{
			var o = testPressed;
			o.set(
				BackgroundProperty
				,new SolidColorBrush(Colors.LightBlue)
			);
		}


		return 0;
	}
}