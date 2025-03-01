using Avalonia.Controls;

namespace ngaq.UI.template;

public partial class Template
	:UserControl
{
	public Template(){
		DataContext = new TemplateVm();
		//_style();
		_render();
	}

	public TemplateVm? ctx{
		get{return DataContext as TemplateVm;}
		set{DataContext = value;}
	}

	protected zero _render(){
		//Content =
		return 0;
	}

}