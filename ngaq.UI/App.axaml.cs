using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using ngaq.UI.viewModels;
using ngaq.UI.views;
using Microsoft.Extensions.DependencyInjection;
using System;
using Avalonia.Styling;
using Avalonia.Controls.Primitives;
using Shr.Avalonia.ext;
using Avalonia.Themes.Fluent;

namespace ngaq.UI;

public partial class App : Application {


	public static IServiceProvider ServiceProvider { get; private set; } = null!;
	public static void ConfigureServices(IServiceProvider serviceProvider){
		ServiceProvider = serviceProvider;
	}

	public override void Initialize() {
		//AvaloniaXamlLoader.Load(this);
		_init();
		_style();
	}

	protected zero _init(){
		RequestedThemeVariant = ThemeVariant.Default;
		Styles.Add(new FluentTheme());
		return 0;
	}

	protected zero _style(){
		var noRoundCorner = new Style(x=>
			x.Is<TemplatedControl>()
		);
		Styles.Add(noRoundCorner);
		noRoundCorner.set(
			TemplatedControl.CornerRadiusProperty
			,new CornerRadius(0)
		);
		return 0;
	}

	public override void OnFrameworkInitializationCompleted() {
		if (ServiceProvider == null){
			//throw new InvalidOperationException("ServiceProvider 未初始化！");
			System.Console.WriteLine("ServiceProvider 未初始化！");
		}

		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
			// Avoid duplicate validations from both Avalonia and the CommunityToolkit.
			// More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
			DisableAvaloniaDataAnnotationValidation();
			desktop.MainWindow = new MainWindow {
				DataContext = new MainViewModel()
				//,SizeToContent = Avalonia.Controls.SizeToContent.WidthAndHeight//據內容 自動調 窗口大小
				,MinWidth=0
				,MinHeight=0
				,Width=1920/2
				,Height=1080/2
			};
		} else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
			singleViewPlatform.MainView = new MainView {
				DataContext = new MainViewModel()
			};
		}

		base.OnFrameworkInitializationCompleted();
	}

	private void DisableAvaloniaDataAnnotationValidation() {
		// Get an array of plugins to remove
		var dataValidationPluginsToRemove =
			BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

		// remove each entry found
		foreach (var plugin in dataValidationPluginsToRemove) {
			BindingPlugins.DataValidators.Remove(plugin);
		}
	}
}
