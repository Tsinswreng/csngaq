using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using model;
using ngaq.Core.model;
using ngaq.Core.model.Sample;
using ngaq.Core.model.wordIF;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.FullWordKv;
using ngaq.UI.viewModels.IF;
using ngaq.UI.viewModels.kv;
using ngaq.UI.views.wordInfo;

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



	protected I_ViewModel<I_KvRow> _kvVm = new KvVm2();
	/// <summary>
	/// current showed kvVm
	/// </summary>
	public I_ViewModel<I_KvRow> kvVm{
		get{return _kvVm;}
		set{SetProperty(ref _kvVm, value);}
	}

	public zero showAtKvView(I_KvRow kv){
		var kvVm = new KvVm2();
		kvVm.fromModel(kv);
		this.kvVm = kvVm;
		return 0;
	}

	public zero showAtWordInfoView(I_FullWordKv fullWord){
		var wordInfoVm = new WordInfoVm();
		wordInfoVm.fromModel(fullWord);
		this.wordInfoVm = wordInfoVm;
		return 0;
	}

	protected I_ViewModel<I_FullWordKv> _wordInfoVm = new WordInfoVm();
	public I_ViewModel<I_FullWordKv> wordInfoVm{
		get{return _wordInfoVm;}
		set{SetProperty(ref _wordInfoVm, value);}
	}


	public zero click_searchBtn(){
		searchedWords.Clear();
		var w = new SearchedWordCardVm();
		w.useSample();//t
		searchedWords.Add(w);

		return 0;
	}
}

