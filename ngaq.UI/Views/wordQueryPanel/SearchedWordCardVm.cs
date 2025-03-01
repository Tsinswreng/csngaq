using model;
using ngaq.Core.Model;
using ngaq.Core.Model.Sample;
using ngaq.Core.Model.wordIF;
using ngaq.UI.viewModels;
using ngaq.UI.viewModels.FullWordKv;
using ngaq.UI.viewModels.IF;

namespace ngaq.UI.viewModels.wordQueryPanel;

public partial class SearchedWordCardVm
	: ViewModelBase
	, I_ViewModel<I_KvRow>
{

	public static SearchedWordCardVm sample{get;set;}
	static SearchedWordCardVm(){
		sample = new SearchedWordCardVm();
		var fullWordKv = FullWordSample.getInst().sample;
		sample.fromModel(fullWordKv.textWord);
	}

	public zero fromModel(I_KvRow model) {
		wordKv = model;
		_init();
		return 0;
	}

	public I_KvRow toModel() {
		return wordKv;
	}

	public I_KvRow wordKv{get;set;}

	public zero useSample(){
		var fullWordKv = FullWordSample.getInst().sample;
		this.fromModel(fullWordKv.textWord);
		return 0;
	}

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