using Avalonia;
using Avalonia.Styling;

namespace Shr.Avalonia.Ext;

public static class StyleExt{
	public static zero set(this Style z, AvaloniaProperty property, object? value){
		z.Setters.Add(new Setter(property, value));
		return 0;
	}
}