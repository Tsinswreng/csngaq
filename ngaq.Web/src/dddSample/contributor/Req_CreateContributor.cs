using System.ComponentModel.DataAnnotations;

namespace ngaq.Web.dddSample.contributor;

public class Req_CreateContributor{
	public const str Route = "/Contributors";

	[Required]
	public str? name{get;set;}
	public str? phoneNumber{get;set;}
}