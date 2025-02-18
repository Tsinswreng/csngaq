using System.ComponentModel;
using Avalonia;
using Avalonia.Data;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;


namespace ngaq.UI.Cmpnt.ScrollInput;
//global using str = System.String;
public partial class ScrollInput : UserControl{
	public ScrollInput(){
		InitializeComponent();
	}

	public static readonly StyledProperty<str> TextProperty
		= AvaloniaProperty.Register<ScrollInput, str>(nameof(Text), defaultBindingMode: BindingMode.TwoWay);

	//[Content]
	public str Text{
		get{return GetValue(TextProperty);}
		set{
			SetValue(TextProperty, value);
		;}
	}

	private void InitializeComponent(){
		AvaloniaXamlLoader.Load(this);
	}

}
