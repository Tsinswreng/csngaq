using Ardalis.SmartEnum;
using S = ngaq.Core.dddSample.contributorAgg.ContributorStatus;
namespace ngaq.Core.dddSample.contributorAgg;

public class ContributorStatus
	:SmartEnum<ContributorStatus>
{
	protected ContributorStatus(str name, i32 value):base(name, value){}
	public static readonly S coreTeam = new(nameof(coreTeam), 1);
	public static readonly S community = new(nameof(community), 2);
	public static readonly S notSet = new(nameof(notSet), 3);

}