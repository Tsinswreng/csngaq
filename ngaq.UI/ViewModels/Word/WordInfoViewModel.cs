using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Model;
using ngaq.Core.Model.consts;
using ngaq.Core.Model.wordIF;
using ngaq.model.consts;
using ngaq.UI.ViewModels;
using ngaq.UI.Views.Word;
using tools;

namespace ngaq.UI.ViewModels.Word;

public partial class WordInfoViewModel :
	ViewModelBase
	,I_RowBaseInfo
{

	// public string kType { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? kStr { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long? kI64 { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? kDesc { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string vType { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? vDesc { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? vStr { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long? vI64 { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public double? vF64 { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long id { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? bl { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? status { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long ct { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long ut { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }

	#region impl

	protected i64 _id;
	public i64 id {
		get => _id;
		set => SetProperty(ref _id, value);
	}

	protected str? _bl;
	public str? bl{
		get => _bl;
		set => SetProperty(ref _bl, value);
	}

	protected i64 _ct;
	public i64 ct{
		get => _ct;
		set => SetProperty(ref _ct, value);
	}

	protected i64 _ut;
	public i64 ut{
		get => _ut;
		set => SetProperty(ref _ut, value);
	}

	protected str? _status;
	public str? status{
		get => _status;
		set => SetProperty(ref _status, value);
	}


	#endregion impl


	public WordInfoViewModel(){

	}

	public WordInfoViewModel(I_FullWordKv word){
		upd_word(word);
	}

	public zero init(){
		if(fullWordKv == null){
			return 0;
		}
		id = fullWordKv.textWord.id;
		Text = fullWordKv?.textWord?.text_()??"";
		bl_props = Tools.classify(
			fullWordKv?.propertys??[]
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

	public zero upd_word(I_FullWordKv word){
		this.fullWordKv = word;
		init();
		return 0;
	}

	public Dictionary<str, IList<I_PropertyKv>> bl_props{get;set;}

	public I_FullWordKv? fullWordKv{get;set;}

	[ObservableProperty]
	private str _text = "test114514";

	[ObservableProperty]
	private str _mean = "";


	public str testLowerCaseBinding{get;set;} = "123";


	[ObservableProperty]
	public str testLowerCaseBinding2 = "123";

}


// interface I_NullableWeight{
// 	public double? weight{get;set;}
// }

// interface I_NonNullWeight:I_NullableWeight{
// 	public double weight{get;set;}
// }


// interface I_NullableWeight<T>{
// 	public T weight{get;set;}
// }

// interface I_NonNullWeight:I_NullableWeight<double>{

// }

// class NullableC:I_NullableWeight<double?>{
// 	public double? weight{get;set;}
// }

// class NonNullC:I_NonNullWeight{
// 	public double weight{get;set;}
// }

// class Test{
// 	void fn(){
// 		I_NullableWeight<double?> nullableWeight = new NullableC();
// 		nullableWeight = new NonNullC();
// 	}
// }
