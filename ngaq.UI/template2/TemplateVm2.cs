using System.Collections.Generic;
using ngaq.UI.viewModels;

namespace Shr.Avalonia;
using Ctx = TemplateVm;
public partial class TemplateVm
	:ViewModelBase
{
	public static List<Ctx> samples = [];
	static TemplateVm(){
		samples.Add(new Ctx{

		});
	}
}