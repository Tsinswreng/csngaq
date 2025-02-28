using ngaq.Core.Model;
using ngaq.Core.Model.Sample;
using ngaq.Core.Model.wordIF;
using ngaq.UI.ViewModels;
using ngaq.UI.ViewModels.FullWordKv;
using ngaq.UI.ViewModels.IF;

namespace ngaq.UI.viewModels.wordQueryPanel;

public partial class SearchedWordCardVm
	: ViewModelBase
	, I_ViewModel<I_WordKv>
{

	public static SearchedWordCardVm sample;
	static SearchedWordCardVm(){
		sample = new SearchedWordCardVm();
		var fullWordKv = FullWordSample.getInst().sample;
		sample.fromModel(fullWordKv.textWord);
	}

	public zero fromModel(I_WordKv model) {
		wordKv = model;
		_init();
		return 0;
	}

	public I_WordKv toModel() {
		return wordKv;
	}

	public I_WordKv wordKv{get;set;}

	protected zero _init(){
		id = wordKv.id;
		bl = wordKv.bl??"";
		if(wordKv is I_TextWordKV textWord){
			text = textWord.text_();
		}else{
			fKey = wordKv.kI64;
			if(wordKv is I_PropertyKv prop){
				text = prop.vStr??"";
			}else if(wordKv is I_LearnKv learn){
				text = learn.vStr??"";
			}
		}
		return 0;
	}

	protected i64 _id;
	public i64 id{
		get => _id;
		set => SetProperty(ref _id, value);
	}

	protected str _bl="";
	public str bl{
		get => _bl;
		set => SetProperty(ref _bl, value);
	}

	protected i64? _fKey;
	public i64? fKey{
		get => _fKey;
		set => SetProperty(ref _fKey, value);
	}

	protected str _text;
	public str text{
		get => _text;
		set => SetProperty(ref _text, value);
	}





}