using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Converters;
using System.Globalization;
using Avalonia;
namespace ngaq.ViewModels;


public partial class MainViewModel : ViewModelBase{
	public MainViewModel(){

	}

	//private string _greetings = "一二三四五六七八九十Welcome to Avalonia!"; //不可、名ˋ須潙_greeting
	//private string _greeting = "一二三四五六七八九十Welcome to Avalonia!";
	//private string ____greeting = "____greeting";
	// private CultureInfo _currentCulture = CultureInfo.CurrentUICulture;
	// public CultureInfo CurrentCulture{
	// 	get => _currentCulture;
	// 	set{
	// 		if(value == _currentCulture) return;
	// 		_currentCulture = value;
	// 		//OnPropertyChanged(nameof(CurrentCulture));
	// 		OnPropertyChanged();//通知UI更新
	// 		ReloadResources();
	// 	}
	// }

	[ObservableProperty]
	private string _greeting = "greeting";
	// public string Greeting{
	// 	get => _greeting;
	// 	set => this.RaiseAndSetIfChanged(ref _greeting, value);
	// }


	private string _buttonText;
	// public string ButtonText{
	// 	get => _buttonText;
	// 	set => this.RaiseAndSetIfChanged(ref _buttonText, value);
	// }



	// private void ReloadResources(){
	// 	var resource = AvaloniaLocator.Current.GetService<IResourceProvider>();
	// 	var resources = resource.GetResources(_currentCulture);
	// 	Greeting = resources["Greeting"] as string;
	// 	ButtonText = resources["ButtonText"] as string;
	// }

	// public void SwitchLanguage(){
	// 	CurrentCulture = CurrentCulture.Name == "zh-CN" ? new CultureInfo("en-US") : new CultureInfo("zh-CN");
	// }
}

/* 

若Text="{Binding Abcdefg}"、
類成員就要定義成 private string _abcdefg = "xxx";
或abcdefg 等
不可Abcdefg
 */