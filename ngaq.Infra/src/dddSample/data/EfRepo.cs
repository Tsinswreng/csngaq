using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace ngaq.Infra.dddSample.data;

public class EfRepo<T>(
	AppDbCtx dbCtx
)
	:RepositoryBase<T>(dbCtx)
	,IReadRepository<T>
	where T:class,IAggregateRoot
{

}

