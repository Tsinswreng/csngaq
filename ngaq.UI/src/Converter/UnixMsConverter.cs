using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Shr.Date;
namespace ngaq.UI.Converter;

public class UnixMsConverter : IValueConverter {

	protected static UnixMsConverter? _inst = null;
	public static UnixMsConverter inst => _inst ??= new UnixMsConverter();


	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if(value is i64 unixMs){
			return DateUtil.unixMsToIso8601(unixMs);//str
		}
		return AvaloniaProperty.UnsetValue;
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) {
		if(value is str iso8601){
			return DateUtil.iso8601ToUnixMs(iso8601);//i64
		}
		return AvaloniaProperty.UnsetValue;
	}
}


