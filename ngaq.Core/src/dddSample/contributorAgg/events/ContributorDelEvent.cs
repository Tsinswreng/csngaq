namespace ngaq.Core.dddSample.contributorAgg.events;
using Ardalis.SharedKernel;
public class ContributorDelEvent(i32 contributorId)
	:DomainEventBase
{
	public i32 contributorId{get;init;}=contributorId;

}