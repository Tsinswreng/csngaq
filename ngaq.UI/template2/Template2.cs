using Avalonia.Controls;

namespace Shr.Avalonia;

using Ctx = ngaq.UI.template.TemplateVm;
public partial class Template
	:UserControl
{

	public Ctx? ctx{
		get{return DataContext as Ctx;}
		set{DataContext = value;}
	}


	public Template(){
		ctx = new Ctx();
		_style();
		_render();
	}

	public class Cls{

	}
	public Cls cls{get;set;} = new Cls();

	protected zero _style(){
		return 0;
	}

	protected zero _render(){
		return 0;
	}


}