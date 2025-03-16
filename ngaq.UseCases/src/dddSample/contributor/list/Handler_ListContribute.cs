using Ardalis.Result;
using Ardalis.SharedKernel;

namespace ngaq.UseCases.dddSample.contributor.list;

public class Handler_ListContributor(
	I_Svc_ListContributor _query
)
	:IQueryHandler<Qry_ListContributor, Result<IEnumerable<Dto_Contributor>>>
{
	public async Task<Result<IEnumerable<Dto_Contributor>>> Handle(
		Qry_ListContributor req
		,CancellationToken ct
	){
		var result = await _query.ListAsy();
		return Result.Success(result);
	}
}