using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using ngaq.ViewModels;
using ngaq.Views;

namespace ngaq {
	public partial class App : Application {
		public override void Initialize() {
			AvaloniaXamlLoader.Load(this);
		}

		public override void OnFrameworkInitializationCompleted() {
			if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
				// Line below is needed to remove Avalonia data validation.
				// Without this line you will get duplicate validations from both Avalonia and CT
				BindingPlugins.DataValidators.RemoveAt(0);
				desktop.MainWindow = new MainWindow {
					DataContext = new MainViewModel()
				};
			}
			else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform) {
				singleViewPlatform.MainView = new MainView {
					DataContext = new MainViewModel()
				};
			}

			base.OnFrameworkInitializationCompleted();
		}
	}
}

/* 
<TextBlock Text="{DynamicResource AppName}" />
<TextBlock Text="{DynamicResource HelloWorld}" />
<TextBlock Text="{DynamicResource Greeting}" />


using Avalonia.Markup.Xaml.Styling;
using System.Globalization;

// ... other code ...

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        var culture = CultureInfo.CurrentCulture;
        var resourcePath = $"Resources/Strings.{culture.Name}.xaml";

        //尝试加载特定语言的资源，如果失败则加载默认资源
        try
        {
            var style = new StyleInclude(new Uri("avares://YourAppName/Styles.xaml")) { Source = new Uri(resourcePath) };
            Styles.Add(style);
        }
        catch
        {
            var style = new StyleInclude(new Uri("avares://YourAppName/Styles.xaml")) { Source = new Uri("avares://YourAppName/Resources/Strings.xaml") };
            Styles.Add(style);
        }
    }

    // ... other code ...
}
 */
