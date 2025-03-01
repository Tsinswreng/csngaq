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
using static Avalonia.Layout.Layoutable;
using Avalonia.Controls.Primitives;
using Avalonia.Media;


namespace ngaq.UI.styles;


public class SimpleTextBoxStyle{

	protected static SimpleTextBoxStyle? _inst = null;
	public static SimpleTextBoxStyle inst => _inst??= new SimpleTextBoxStyle();

	public Style set(Style o){
		//
		o.set(
			MarginProperty
			,new Thickness(0)
		);
		o.set(
			TemplatedControl.PaddingProperty
			,new Thickness(0)
		);
		o.set(
			MinHeightProperty
			,0.0
		);
		o.set(
			MinWidthProperty
			,0.0
		);
		o.set(
			TemplatedControl.BorderThicknessProperty
			,new Thickness(0)
		);
		o.set(
			TextBox.TextWrappingProperty
			,TextWrapping.Wrap
		);
		//
		return o;
	}
}