using CommunityToolkit.Mvvm.ComponentModel;

namespace ngaq.ViewModels;


public partial class MainViewModel : ViewModelBase{
	// 類於vue之ref?
	[ObservableProperty]
	private string greeting = "greeting";

	//private string _greetings = "一二三四五六七八九十Welcome to Avalonia!"; //不可、名ˋ須潙_greeting
	//private string _greeting = "一二三四五六七八九十Welcome to Avalonia!";
	//private string ____greeting = "____greeting";

}

/* 

若Text="{Binding Abcdefg}"、
類成員就要定義成 private string _abcdefg = "xxx";
或abcdefg 等
不可Abcdefg
 */