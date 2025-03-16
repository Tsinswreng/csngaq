using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace ngaq.Core.dddSample.contributorAgg;

public class Contributor(str name)
	:EntityBase
	,IAggregateRoot
{
  // Example of validating primary constructor inputs
  // See: https://learn.microsoft.com/en-us/dotnet/csharp/whats-new/tutorials/primary-constructors#initialize-base-class
	public str name{get;protected set;} = Guard.Against.NullOrEmpty(name, nameof(name));
	public ContributorStatus status{get;protected set;} = ContributorStatus.notSet;
	public PhoneNumber? phoneNumber{get;protected set;}
	public zero setPhoneNumber(str phoneNumber){
		this.phoneNumber = new PhoneNumber("", phoneNumber, "");
		return 0;
	}

	public zero updName(str neoName){
		this.name = Guard.Against.NullOrEmpty(neoName, nameof(neoName));
		return 0;
	}


}


public class PhoneNumber(
	str countryCode
	,str number
	,str? extension
)	:ValueObject
{
	public str countryCode{get;protected set;} = countryCode;
	public str number{get;protected set;} = number;
	public str? extension{get;protected set;} = extension;

	protected override IEnumerable<object> GetEqualityComponents(){
		yield return countryCode;
		yield return number;
		yield return extension ?? String.Empty;
	}
}