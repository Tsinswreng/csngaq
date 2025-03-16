using Ardalis.Result;
using Ardalis.SharedKernel;
using ngaq.Core.dddSample.contributorAgg;
using ngaq.Core.dddSample.contributorAgg.specs;

namespace ngaq.UseCases.dddSample.contributor.get;

/// Queries don't necessarily need to use repository methods, but they can if it's convenient
public class GetHandler_Contributor(
	IReadRepository<Contributor> _repo
)
	:IQueryHandler<GetQry_Contributor, Result<Dto_Contributor>>
{
	public async Task<Result<Dto_Contributor>> Handle(
		GetQry_Contributor req
		,CancellationToken ct
	){
		var spec = new Spec_ContributorById(req.contributorId);
		var entity = await _repo.FirstOrDefaultAsync(spec, ct);
		if(entity == null){
			return Result.NotFound();
		}
		return new Dto_Contributor(entity.Id, entity.name, entity.phoneNumber?.number??"");
	}

}