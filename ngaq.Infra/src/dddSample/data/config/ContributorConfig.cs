using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ngaq.Core.dddSample.contributorAgg;

namespace ngaq.Infra.dddSample.data.config;

public class ContributorConfig
	:IEntityTypeConfiguration<Contributor>
{

	public void Configure(EntityTypeBuilder<Contributor> b){
		b.Property(p=>p.name)
			.HasMaxLength(DataSchemaConsts.DEFAULT_NAME_LENGTH)
			.IsRequired()
		;

		b.OwnsOne(b=>b.phoneNumber);

		b.Property(x=>x.status)
			.HasConversion(
				x=>x.Value//程序對象轉數據庫對象
				,x=>ContributorStatus.FromValue(x)//數據庫對象轉程序對象
			)
		;
	}
}
