using Ardalis.Result;
using Ardalis.SharedKernel;

namespace ngaq.UseCases.dddSample.contributor.list;


public record Qry_ListContributor(
	i32? skip
	,i32? take
)
	:IQuery<Result<IEnumerable<Dto_Contributor>>>
{

}