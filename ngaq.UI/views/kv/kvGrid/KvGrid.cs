using Avalonia.Controls;
using Avalonia.Data;
using ngaq.UI.views.kv.kvGrid;

namespace ngaq.UI.kv.kvGrid;

public partial class KvGrid
	:UserControl
{
	public KvGrid(){
		//DataContext = new KvGridVm();
		DataContext = KvGridVm.samples[1];
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
		{
			var o = ans;
			o.Bind(
				DataGrid.ItemsSourceProperty
				,new Binding(nameof(ctx.kvs))
			);
		}
		{{

		}}//~ans
		return 0;
	}

}