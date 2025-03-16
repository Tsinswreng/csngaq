using Ardalis.Result;
using Ardalis.SharedKernel;

namespace ngaq.UseCases.dddSample.contributor.update;

public record UpdCmd_Contributor(
	i32 contributorId
	,str neoName
):ICommand<Result<Dto_Contributor>>{}