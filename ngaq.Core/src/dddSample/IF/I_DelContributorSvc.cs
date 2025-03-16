using Ardalis.Result;

namespace ngaq.Core.dddSample.IF;

public interface I_DelContributorSvc{
  // This service and method exist to provide a place in which to fire domain events
  // when deleting this aggregate root entity
	public Task<Result> DelContributor(i32 contributorId);
}