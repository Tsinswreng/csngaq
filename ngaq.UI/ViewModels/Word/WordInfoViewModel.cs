using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.model;
using ngaq.Core.model.consts;
using ngaq.Core.model.wordIF;
using ngaq.model.consts;
using ngaq.UI.ViewModels;
using ngaq.UI.Views.Word;
using tools;

namespace ngaq.UI.ViewModels.Word;

public partial class WordInfoViewModel :
	ViewModelBase
{
	public WordInfoViewModel(){

	}

	public WordInfoViewModel(I_FullWordKV word){
		upd_word(word);
	}

	public zero init(){
		Text = joinedWordKV?.textWord?.text_()??"";
		bl_props = Tools.classify(
			joinedWordKV?.propertys??[]
			,e=>e.bl
		);
		var bl_mean = BlPrefix.join(
			BlPrefix.Property
			,PropertyEnum.mean.ToString()
		);
		var meanProp = bl_props?.GetValueOrDefault(bl_mean);
		Mean = meanProp?[0].vStr??"";
		return 0;
	}

	public zero upd_word(I_FullWordKV word){
		this.joinedWordKV = word;
		init();
		return 0;
	}

	public Dictionary<str, IList<I_PropertyKV>> bl_props{get;set;}

	public I_FullWordKV? joinedWordKV{get;set;}

	[ObservableProperty]
	private str _text = "test114514";

	[ObservableProperty]
	private str _mean = "";


	public str testLowerCaseBinding{get;set;} = "123";

	[ObservableProperty]
	public str testLowerCaseBinding2 = "123";

}
