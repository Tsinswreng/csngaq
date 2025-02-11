using Avalonia.Controls;
using ngaq.UI.ViewModels.FullWordKv;

namespace ngaq.UI.Views.FullWordKvView;

public partial class FullWordKvView : UserControl{
	public FullWordKvView(){
		InitializeComponent();
		this.DataContext = new FullWordKv();
	}
}
