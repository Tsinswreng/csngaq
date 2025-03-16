using Ardalis.Result;
using Ardalis.SharedKernel;
using ngaq.Core.dddSample.IF;

namespace ngaq.UseCases.dddSample.contributor.delete;

public class DelHandler_Contributor(
	I_DelSvc_Contributor _delSvc
)
	:ICommandHandler<DelCmd_Contributor, Result>
{

	public async Task<Result> Handle(
		DelCmd_Contributor req
		,CancellationToken ct
	){
    // This Approach: Keep Domain Events in the Domain Model / Core project; this becomes a pass-through
    // This is @ardalis's preferred approach
		return await _delSvc.DelContributor(req.contributorId);
    // Another Approach: Do the real work here including dispatching domain events - change the event from internal to public
    // @ardalis prefers using the service above so that **domain** event behavior remains in the **domain model** (core project)
    // var aggregateToDelete = await _repository.GetByIdAsync(request.ContributorId);
    // if (aggregateToDelete == null) return Result.NotFound();
    // await _repository.DeleteAsync(aggregateToDelete);
    // var domainEvent = new ContributorDeletedEvent(request.ContributorId);
    // await _mediator.Publish(domainEvent);// return Result.Success();
	}

}
