using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;
using ngaq.UI.views.kv;
using Shr.Avalonia.ext;

namespace ngaq.UI.views.wordQueryPanel;

public partial class WordQueryPanel{

	protected zero _style(){

		var stretch = new Style(x=>
			x.Is<Control>()
			.Class(cls.Stretch)
		);
		Styles.Add(stretch);
		{
			var o = stretch;
			o.set(
				HorizontalAlignmentProperty
				,Avalonia.Layout.HorizontalAlignment.Stretch
			);
		}

		var wordCardStrentch = new Style(x=>
			x.OfType<SearchedWordCard>()
			.Descendant()
			.OfType<Button>()
		);
		Styles.Add(wordCardStrentch);
		{
			var o = wordCardStrentch;
			o.set(
				HorizontalAlignmentProperty
				,HorizontalAlignment.Stretch
			);
		}

		var kvView = new Style(x=>
			x.OfType<KvView>()
		);
		Styles.Add(kvView);
		{
			var o = kvView;
			o.set(
				FontSizeProperty
				,16.0
			);
		}


		var girdShowLine = new Style(x=>
			x.Is<Grid>()
		);
		//Styles.Add(girdShowLine);
		{
			var o = girdShowLine;
			o.set(
				Grid.ShowGridLinesProperty
				,true
			);
		}
		return 0;
	}
}