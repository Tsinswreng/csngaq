using Microsoft.EntityFrameworkCore;
using ngaq.UseCases.dddSample.contributor;
using ngaq.UseCases.dddSample.contributor.list;

namespace ngaq.Infra.dddSample.data.querys;

public class Svc_ListContributor(
	AppDbCtx _db
)
	:I_Svc_ListContributor
{

	// You can use EF, Dapper, SqlClient, etc. for queries -
  // this is just an example
	public async Task<IEnumerable<Dto_Contributor>> ListAsy(){
		var result = await _db.Database.SqlQuery<Dto_Contributor>(
			$"SELECT Id, Name, PhoneNumber_Number AS PhoneNumber FROM Contributors" // don't fetch other big columns
		).ToListAsync();
		return result;
	}
}