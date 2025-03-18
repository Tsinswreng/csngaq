using Microsoft.EntityFrameworkCore;
using ngaq.Core.dddSample.contributorAgg;

namespace ngaq.Infra.dddSample.data;
/// <summary>
/// AI曰 播種旹 向數據庫中插入預設賬號(管理員)等
/// </summary>

public static class SeedData{
	public static readonly Contributor contributor1 = new("Ardalis");
	public static readonly Contributor contributor2 = new("SnowFrog");

	public static async Task InitAsy(AppDbCtx dbCtx){
		if(await dbCtx.contributors.AnyAsync()){//// DB has been seeded
			return;
		}
		await PopulateTestDataAsy(dbCtx);
	}

	public static async Task PopulateTestDataAsy(AppDbCtx dbCtx){
		dbCtx.contributors.AddRange([contributor1, contributor2]);
		await dbCtx.SaveChangesAsync();
	}

}