namespace ngaq.Core.dddSample.contributorAgg.handlers;
using MediatR;
using Microsoft.Extensions.Logging;
using ngaq.Core.dddSample.contributorAgg.events;
using ngaq.Core.dddSample.IF;
/// <summary>
/// A domain event that is dispatched whenever a contributor is deleted.
/// The DeleteContributorService is used to dispatch this event.
/// </summary>
public class ContributorDelHandler(
	ILogger<ContributorDelHandler> logger
	,I_sendEmailAsy emailSender
)	:INotificationHandler<ContributorDelEvent>
{

	public async Task Handle(
		ContributorDelEvent domainEvent
		,CancellationToken cancellationToken
	){
		logger.LogInformation(
			"Handling Contributed Deleted event for {contributorId}"
			,domainEvent.contributorId
		);
		await emailSender.sendEmailAsy(
			""
			,""
			,""
			,""
		);
	}

}