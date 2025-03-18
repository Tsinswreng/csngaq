namespace ngaq.UseCases.dddSample.contributor.list;

/// <summary>
/// Represents a service that will actually fetch the necessary data
/// Typically implemented in Infrastructure
/// </summary>
public interface I_Svc_ListContributor{
	Task<IEnumerable<Dto_Contributor>> ListAsy();
}