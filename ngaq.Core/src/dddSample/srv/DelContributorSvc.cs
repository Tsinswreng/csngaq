using Ardalis.Result;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.Logging;
using ngaq.Core.dddSample.contributorAgg;
using ngaq.Core.dddSample.contributorAgg.events;
using ngaq.Core.dddSample.IF;

namespace ngaq.Core.dddSample.srv;

public class DelContributorSvc(
	IRepository<Contributor> _repository
	,IMediator _mediator
	,ILogger<DelContributorSvc> _logger
)
	:I_DelContributorSvc
{
	public async Task<Result> DelContributor(i32 contributorId){
		_logger.LogInformation("Deleting contributor with id {contributorId}", contributorId);
		var aggToDel = await _repository.GetByIdAsync(contributorId);
		if(aggToDel == null){
			return Result.NotFound();
		}
		await _repository.DeleteAsync(aggToDel);
		var domainEvent = new ContributorDelEvent(contributorId);
		await _mediator.Publish(domainEvent);
		return Result.Success();
	}
}