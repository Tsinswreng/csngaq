using Avalonia.Controls;

namespace ngaq.UI.Template.Views;

public partial class TemplateView : UserControl{
	public TemplateView(){
		InitializeComponent();
		this.DataContext = new object();//TODO
	}
}
