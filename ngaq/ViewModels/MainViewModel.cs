using CommunityToolkit.Mvvm.ComponentModel;

namespace ngaq.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private string _greeting = "一二三四五六七八九十Welcome to Avalonia!";
    }
}
