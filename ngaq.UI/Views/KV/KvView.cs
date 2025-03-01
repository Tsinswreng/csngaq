/*
爲甚麼我把_style函數內容註釋掉後、所有的ScrollInput都變成左對齊的、
但是恢復_style函數後、SCrollInput就全部變成居中的了? 其他控件不受影響
 */
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Interactivity;
using Avalonia.Styling;
using ngaq.Core.Model;
using ngaq.Core.Model.Sample;
using ngaq.UI.Cmpnt.ScrollInput;
using ngaq.UI.Converter;
using ngaq.UI.viewModels.KV;
using Avalonia.Layout;
using ngaq.UI.viewModels.kv;
using Shr.Avalonia.ext;
using ngaq.UI.styles;
using Avalonia.Media;
using Shr.Avalonia.util;

namespace ngaq.UI.views.kv;

public partial class KvView : UserControl{
	public enum ClsEnum{
		InnerStackPanel
	}

	public class Cls{
		public str Label = nameof(Cls.Label);
	}

	public Cls cls = new Cls();

	public KvView(){
		DataContext = new KvVm();
		_style();
		_render();
	}

	public KvVm2? ctx{
		get{return DataContext as KvVm2;}
	}

	protected zero _style(){

		var noMarginPadding = new Style(x=>
			x.Is<Control>()
		);
		Styles.Add(noMarginPadding);
		{
			var o = noMarginPadding;
			NoMarginPaddingStyle.inst.set(o);
		}

		var label = new Style(x=>
			x.Is<Control>()
			.Class(cls.Label)
		);
		Styles.Add(label);
		{
			var o = label;
			o.set(
				FontSizeProperty
				,14.0
			);
			o.set(
				ForegroundProperty
				,Brushes.Gray
			);

		}

		var textBox = new Style(
			x=>x.Is<TextBox>()
		);
		Styles.Add(textBox);
		{//sty_scrollInput
			var o = textBox;
			o.set(
				HorizontalAlignmentProperty
				,HorizontalAlignment.Stretch
			);
			//SimpleTextBoxStyle.inst.set(o);
			o.set(
				BorderBrushProperty
				,Brushes.Gray
			);
			o.set(
				BorderThicknessProperty
				//,new Thickness(0,0,0,1)
				,new Thickness(1)
			);
			//
			o.set(
				MinHeightProperty
				,0.0
			);
			o.set(
				MinWidthProperty
				,0.0
			);
			o.set(
				MaxHeightProperty
				,64.0
			);

			o.set(
				TextBox.AcceptsReturnProperty
				,true
			);
			//

			// sty_scrollInput.Setters.Add(new Setter(
			// 	ScrollInput.WidthProperty
			// 	,100.0
			// ));
			// sty_scrollInput.Setters.Add(new Setter(
			// 	ScrollInput.MaxHeightProperty
			// 	,40.0
			// ));
			// o.set(
			// 	ScrollInput.HorizontalAlignmentProperty
			// 	,HorizontalAlignment.Left
 			// );
		}//~sty_scrollInput
		var textBoxFocus = new Style(x=>
			x.Is<TextBox>()
			.Class(PsdCls.inst.focus)
		);
		Styles.Add(textBoxFocus);
		{
			var o = textBoxFocus;
			// o.set(
			// 	BackgroundProperty
			// 	,Brushes.LightGray
			// );
			o.set(
				MaxHeightProperty
				,9999.0
			);
			o.set(
				HeightProperty
				,256.0
			);
		}

		return 0;
	}

	protected zero _render(){
		var outer = new StackPanel(){
			Margin = new Thickness(10)
		};
		Content = outer;
		{
			var o = outer;
			o.Bind(
				IsVisibleProperty
				,new Binding(nameof(ctx.hasValue))
			);
		}
		var oneKvBox = (
			str title
			,str bindingName
			,IValueConverter? converter
		)=>{
			var box = new StackPanel(){};
			box.Classes.Add(nameof(ClsEnum.InnerStackPanel));
			{//box:StackPanel
				var titleBlock = new TextBlock(){
					Text = title
				};
				box.Children.Add(titleBlock);
				{
					var o = titleBlock;
					o.Classes.Add(cls.Label);
				}
				//
				var scrollInput = new TextBox();
				scrollInput.Bind(
					TextBox.TextProperty
					,new Binding(bindingName){
						Mode = BindingMode.TwoWay
						,Converter = converter
					}
				);
				box.Children.Add(scrollInput);
			}//~box:StackPanel
			return box;
		};//~oneKvBox:Func
		var idBox = oneKvBox("id",nameof(ctx.id),null);
		outer.Children.Add(idBox);
		//
		var blBox = oneKvBox("bl",nameof(ctx.bl),null);
		outer.Children.Add(blBox);
		//
		var ctBox = oneKvBox("ct",nameof(ctx.ct), UnixMsConverter.inst);
		outer.Children.Add(ctBox);
		//
		var utBox = oneKvBox("ut",nameof(ctx.ut), UnixMsConverter.inst);
		outer.Children.Add(utBox);
		//
		var statusBox = oneKvBox("status",nameof(ctx.status),null);
		outer.Children.Add(statusBox);
		//
		var kTypeBox = oneKvBox("kType",nameof(ctx.kType),null);
		outer.Children.Add(kTypeBox);
		//
		var kDescBox = oneKvBox("kDesc",nameof(ctx.kDesc),null);
		outer.Children.Add(kDescBox);
		//
		var kI64Box = oneKvBox("kI64",nameof(ctx.kI64),null);
		outer.Children.Add(kI64Box);
		//
		var kStrBox = oneKvBox("kStr",nameof(ctx.kStr),null);
		outer.Children.Add(kStrBox);
		//
		var vTypeBox = oneKvBox("vType",nameof(ctx.vType),null);
		outer.Children.Add(vTypeBox);
		//
		var vDescBox = oneKvBox("vDesc",nameof(ctx.vDesc),null);
		outer.Children.Add(vDescBox);
		//
		var vStrBox = oneKvBox("vStr",nameof(ctx.vStr),null);
		outer.Children.Add(vStrBox);
		//
		var vI64Box = oneKvBox("vI64",nameof(ctx.vI64),null);
		outer.Children.Add(vI64Box);
		//
		var vF64Box = oneKvBox("vF64",nameof(ctx.vF64),null);
		outer.Children.Add(vF64Box);
		//
		return 0;
	}

}