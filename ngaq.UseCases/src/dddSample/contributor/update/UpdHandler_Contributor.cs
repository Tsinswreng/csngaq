using Ardalis.Result;
using Ardalis.SharedKernel;
using ngaq.Core.dddSample.contributorAgg;

namespace ngaq.UseCases.dddSample.contributor.update;

public class UpdHandler_Contributor(
	IRepository<Contributor> _repo
)
	:ICommandHandler<UpdCmd_Contributor, Result<Dto_Contributor>>
{

	public async Task<Result<Dto_Contributor>> Handle(
		UpdCmd_Contributor req
		,CancellationToken ct
	){
		var existingContributor = await _repo.GetByIdAsync(req.contributorId, ct);
		if(existingContributor == null){
			return Result.NotFound();
		}
		existingContributor.updName(req.neoName);
		await _repo.UpdateAsync(existingContributor, ct);
		return new Dto_Contributor(
			existingContributor.Id
			,existingContributor.name
			,existingContributor.phoneNumber?.number??""
		);

	}
}