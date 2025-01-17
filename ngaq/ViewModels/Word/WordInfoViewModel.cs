using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.model;
using ngaq.Core.model.consts;
using ngaq.Core.model.wordIF;
using ngaq.model.consts;
using tools;

namespace ngaq.ViewModels.Word;

public partial class WordInfoViewModel :
	ViewModelBase
{
	public WordInfoViewModel(){

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

	public zero upd_joinedWordKV(I_JoinedWordKV joinedWordKV){
		this.joinedWordKV = joinedWordKV;
		init();
		return 0;
	}

	public Dictionary<str, IList<I_PropertyKV>> bl_props{get;set;}

	[ObservableProperty]
	private string _textBoxAText = "114514 to Avalonia!";

	[ObservableProperty]
	private string _textBoxBText = "";


	public I_JoinedWordKV? joinedWordKV{get;set;}

	[ObservableProperty]
	private str _text = "test114514";

	[ObservableProperty]
	private str _mean = "";


}
