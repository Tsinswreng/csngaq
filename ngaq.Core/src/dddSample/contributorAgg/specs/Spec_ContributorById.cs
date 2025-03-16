using Ardalis.Specification;

namespace ngaq.Core.dddSample.contributorAgg.specs;

public class Spec_ContributorById
	:Specification<Contributor>
{
	public Spec_ContributorById(i32 contributorId){
		Query.Where(e=>e.Id == contributorId);
	}
}