using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Styling;
using Shr.Avalonia.ext;

namespace ngaq.UI.styles;

public class NoMarginPaddingStyle{

	protected static NoMarginPaddingStyle? _inst = null;
	public static NoMarginPaddingStyle inst => _inst??= new NoMarginPaddingStyle();


	public Style set(Style o){
		o.set(
			TemplatedControl.MarginProperty
			,new Thickness(0)
		);
		o.set(
			TemplatedControl.PaddingProperty
			,new Thickness(0)
		);
		return o;
	}
}