using System.Collections.Generic;
using ngaq.UI.viewModels;

namespace ngaq.UI.views.user.login;
using Ctx = LoginVm;
public partial class LoginVm
	:ViewModelBase
{
	public static List<Ctx> samples = [];
	static LoginVm(){
		samples.Add(new Ctx{

		});
	}
}