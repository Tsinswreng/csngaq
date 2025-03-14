using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Styling;
using Shr.Avalonia.ext;

namespace ngaq.UI.kv.kvGrid;

public partial class KvGrid{

	protected zero _style(){
		var dataGrid = new Style(x=>
			x.Is<DataGrid>()
		);
		Styles.Add(dataGrid);
		{
			var o = dataGrid;
			o.set(
				DataGrid.AutoGenerateColumnsProperty
				,true
			);
			o.set(
				DataGrid.IsReadOnlyProperty
				,false
			);
			o.set(
				DataGrid.CanUserReorderColumnsProperty
				,true
			);
			o.set(
				DataGrid.GridLinesVisibilityProperty
				,DataGridGridLinesVisibility.All
			);
			o.set(
				BorderThicknessProperty
				,new Thickness(1)
			);
			o.set(
				BorderBrushProperty
				,Brushes.LightGray
			);
		}
		return 0;
	}
}