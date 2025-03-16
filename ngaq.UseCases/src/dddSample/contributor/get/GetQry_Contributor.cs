using Ardalis.Result;
using Ardalis.SharedKernel;

namespace ngaq.UseCases.dddSample.contributor.get;

public record GetQry_Contributor(i32 contributorId)
	:IQuery<Result<Dto_Contributor>>
;