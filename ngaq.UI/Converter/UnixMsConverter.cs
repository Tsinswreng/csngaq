using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
namespace ngaq.UI.Converter;

public class UnixMsConverter : IValueConverter {
	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if(value is i64 unixMs){
			
		}

		return AvaloniaProperty.UnsetValue;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
		throw new NotImplementedException();
	}
}


