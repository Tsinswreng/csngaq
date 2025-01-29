using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.model;
using ngaq.UI.ViewModels;
namespace ngaq.UI.ViewModels.KV;

public partial class KvVM
	: ViewModelBase
	, I_RowBaseInfo {
	// public long id { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? bl { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public string? status { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long ct { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }
	// public long ut { get => throw new std.NotImplementedException(); set => throw new std.NotImplementedException(); }

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


}


