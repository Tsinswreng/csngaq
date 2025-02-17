using System;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using ngaq.UI;
namespace ngaq.Desktop;

sealed class Program {
	// Initialization code. Don't use any Avalonia, third-party APIs or any
	// SynchronizationContext-reliant code before AppMain is called: things aren't initialized
	// yet and stuff might break.
	[STAThread]
	public static void Main(string[] args){
		var services = new ServiceCollection();
		new Setup().ConfigureServices(services);
		BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
	}

	// Avalonia configuration, don't remove; also used by visual designer.
	public static AppBuilder BuildAvaloniaApp()
		=> AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.WithInterFont()
			.LogToTrace();
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