using Avalonia.Controls;

namespace ngaq.UI.kv.kvGrid;

public partial class KvGrid
	:UserControl
{
	public KvGrid(){
		_style();
		_render();
	}

	public KvGridVm? ctx{
		get{return DataContext as KvGridVm;}
		set{DataContext = value;}
	}

	protected zero _render(){
		var ans = new DataGrid();
		Content = ans;
		{{

		}}//~ans
		return 0;
	}

}