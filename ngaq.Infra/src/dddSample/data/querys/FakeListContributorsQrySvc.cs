using ngaq.UseCases.dddSample.contributor;
using ngaq.UseCases.dddSample.contributor.list;

namespace ngaq.Infra.dddSample.data.querys;

public class FakeListContributorsQrySvc
	:I_Svc_ListContributor
{
	public Task<IEnumerable<Dto_Contributor>> ListAsy(){
		IEnumerable<Dto_Contributor> result = [
			new Dto_Contributor(1, "Fake Contributor 1", "")
			,new Dto_Contributor(2, "Fake Contributor 2", "")
		];
		return Task.FromResult(result);
	}
}

