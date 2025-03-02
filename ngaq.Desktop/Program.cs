using System;
using Avalonia;
using Avalonia.Media;
using Microsoft.Extensions.DependencyInjection;
using ngaq.Core.svc.crud.wordCrud.IF;
using ngaq.Server.Svc.Crud.WordCrud;
using ngaq.UI;
using ngaq.UI.viewModels.WordCrud;
namespace ngaq.Desktop;

sealed class Program {
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args){
		var services = new ServiceCollection();
		//new Setup().ConfigureServices(services);
		services.AddSingleton<I_SeekFullWordKVByIdAsy, WordSeeker>();
		services.AddTransient<WordCrudVm>();
		var servicesProvider = services.BuildServiceProvider();

		BuildAvaloniaApp()
			.AfterSetup(e=>App.ConfigureServices(servicesProvider))
			.StartWithClassicDesktopLifetime(args);
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp(){
		// var fontMgrOpt = new FontManagerOptions{
		// 	DefaultFamilyName = "孤鹜 筑紫明朝"
		// };
		return AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.WithInterFont()
			//.With(fontMgrOpt)
			.LogToTrace();
		}
	}


// unsafe{
// 	int i = 0;
// 	int* p1,p2,p3;
// 	p1 = &i;//ok
// 	p2 = &i;//ok
// 	p3 = &i;//ok

// 	int num;
// 	num = &i;//无法将类型“int*”隐式转换为“int”。存在一个显式转换(是否缺少强制转换?)CS0266
// }


