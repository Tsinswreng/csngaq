using Ardalis.Result;

namespace ngaq.UseCases.dddSample.contributor.create;

public record CreateCmd_Contributor(
	str name
	,str? phoneNumber
)
	:Ardalis.SharedKernel.ICommand<Result<i32>>
{}
