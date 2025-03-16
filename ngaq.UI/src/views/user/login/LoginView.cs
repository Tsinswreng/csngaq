using Avalonia.Controls;

namespace ngaq.UI.views.user.login;

using Ctx = LoginVm;
public partial class LoginView
	:UserControl
{

	public Ctx? ctx{
		get{return DataContext as Ctx;}
		set{DataContext = value;}
	}


	public LoginView(){
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
		Content = "login";
		return 0;
	}


}