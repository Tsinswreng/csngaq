using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Model.wordIF;
using ngaq.Core.Svc.Crud.WordCrud.IF;
using ngaq.UI.viewModels.FullWordKv;
using ngaq.UI.viewModels.IF;
using ngaq.UI.viewModels.WordInfo;
using ngaq.UI.views.WordInfo;

namespace ngaq.UI.viewModels.WordCrud;

public partial class WordCrudVm
	:ViewModelBase
	,I_ViewModel<I_FullWordKv>
{


	public zero fromModel(I_FullWordKv model) {
		_fullWordKv = model;
		fullWordKvVm.fromModel(model);
		wordInfoVm.fromModel(model);
		return 0;
	}

	public I_FullWordKv toModel() {
		return _fullWordKv;
	}

	protected I_FullWordKv _fullWordKv{get;set;}

	public WordCrudVm(){}
	public WordCrudVm(I_SeekFullWordKVByIdAsy wordSeeker){
		this.wordSeeker = wordSeeker;
	}

	public I_SeekFullWordKVByIdAsy wordSeeker{get;set;} = null!;


	protected str _searchId="191235";
	public str searchId{
		get => _searchId;
		set => SetProperty(ref _searchId, value);
	}

	protected FullWordKvVm _fullWordKvVm = new FullWordKvVm();
	public FullWordKvVm fullWordKvVm{
		get => _fullWordKvVm;
		set => SetProperty(ref _fullWordKvVm, value);
	}

	protected WordInfoVm _wordInfoVm = new WordInfoVm();
	public WordInfoVm wordInfoVm{
		get => _wordInfoVm;
		set => SetProperty(ref _wordInfoVm, value);
	}


	public async Task<zero> seekFullWordKvByIdAsync(){
		try{
			var inputIdNum = i64.Parse(searchId);
			var ans = await wordSeeker.SeekFullWordKVByIdAsy(inputIdNum);
			if(ans == null){
				//TODO
				return 0;
			}
			//fullWordKvVm.fromModel(ans);
			fromModel(ans);
		}
		catch (System.Exception e){
			G.log(e);//TODO
			throw;
		}
		return 0;

	}
}

