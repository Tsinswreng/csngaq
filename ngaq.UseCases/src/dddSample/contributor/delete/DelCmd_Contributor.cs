using Ardalis.Result;
using Ardalis.SharedKernel;

namespace ngaq.UseCases.dddSample.contributor.delete;

public record DelCmd_Contributor(int contributorId)
	:ICommand<Result>;