using Ardalis.Result;
using Ardalis.SharedKernel;
using ngaq.Core.dddSample.contributorAgg;

namespace ngaq.UseCases.dddSample.contributor.create;

public class CreateCmdHandler_Contributor(
	IRepository<Contributor> _repository
)
	:ICommandHandler<CreateCmd_Contributor, Result<i32>>
{

	public async Task<Result<i32>> Handle(
		CreateCmd_Contributor req
		,CancellationToken ct
	){
		var neoContributor = new Contributor(req.name);
		if(!str.IsNullOrEmpty(req.phoneNumber)){
			neoContributor.setPhoneNumber(req.phoneNumber);
		}
		var createdItem = await _repository.AddAsync(neoContributor, ct);
		return createdItem.Id;//Result有隱式轉換
	}

}