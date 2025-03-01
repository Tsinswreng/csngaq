using System.Collections.ObjectModel;
using ngaq.Core.Model;
using ngaq.Core.Model.Sample;
using ngaq.Core.Model.wordIF;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.FullWordKv;
using ngaq.UI.viewModels.IF;

namespace ngaq.UI.viewModels.wordQueryPanel;

public class WordQueryPanelVm
	:ViewModelBase
{

	public WordQueryPanelVm(){

	}

	protected ObservableCollection<SearchedWordCardVm> _searchedWords = new();
	public ObservableCollection<SearchedWordCardVm> searchedWords{
		get => _searchedWords;
		set => SetProperty(ref _searchedWords, value);
	}





	public zero click_searchBtn(){
		searchedWords.Clear();
		var w = new SearchedWordCardVm();
		w.useSample();
		searchedWords.Add(w);
		return 0;
	}

}