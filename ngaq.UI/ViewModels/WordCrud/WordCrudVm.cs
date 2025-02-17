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


	protected str _searchId="";
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
			var inputIdNum = i64.Parse(searchId);
			var ans = await wordSeeker.SeekFullWordKVByIdAsy(inputIdNum);
			if(ans == null){
				//TODO
				return 0;
			}
			fullWordKvVm.fromModel(ans);
		}
		catch (System.Exception e){
			G.log(e);//TODO
			throw;
		}
		return 0;

	}
}

