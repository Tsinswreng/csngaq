using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ngaq.Core.Svc.Crud.WordCrud.IF;
using ngaq.UI.ViewModels.FullWordKv;

namespace ngaq.UI.ViewModels.WordCrud;

public partial class WordCrudVm
	:ViewModelBase
{

	public WordCrudVm(){}
	public WordCrudVm(I_SeekFullWordKVByIdAsy wordSeeker){
		this.wordSeeker = wordSeeker;
	}

	public I_SeekFullWordKVByIdAsy wordSeeker{get;set;} = null!;


	protected str _searchId="init";
	public str searchId{
		get => _searchId;
		set => SetProperty(ref _searchId, value);
	}

	protected FullWordKvVm _fullWordKvVm = new FullWordKvVm();
	public FullWordKvVm fullWordKvVm{
		get => _fullWordKvVm;
		set => SetProperty(ref _fullWordKvVm, value);
	}

	public async Task<zero> seekFullWordKvByIdAsync(){
		try{
			//
			G.log(1);
			G.log(wordSeeker==null);
			G.log(wordSeeker);
			// var ans = await wordSeeker.SeekFullWordKVByIdAsy(1);
			// G.log(ans.ToString());
			//G.logJson(ans);
		}
		catch (System.Exception e){
			G.log(e);
			throw;
		}
		return 0;
	}




}