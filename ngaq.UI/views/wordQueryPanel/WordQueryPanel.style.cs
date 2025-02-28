using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Styling;
using Shr.Avalonia.ext;

namespace ngaq.UI.views.wordQueryPanel;

public partial class WordQueryPanel{

	protected zero _style(){
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
		return 0;
	}
}