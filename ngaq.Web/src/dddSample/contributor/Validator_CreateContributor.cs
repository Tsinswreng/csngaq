using FastEndpoints;
using FluentValidation;
using ngaq.Infra.dddSample.data.config;

namespace ngaq.Web.dddSample.contributor;

/// See: https://fast-endpoints.com/docs/validation
public class Validator_CreateContributor
	:Validator<Req_CreateContributor>

{

	public Validator_CreateContributor()
	{
		RuleFor(x=>x.name)
			.NotEmpty()
			.WithMessage("name is required")
			.MinimumLength(2)
			.MaximumLength(DataSchemaConsts.DEFAULT_NAME_LENGTH)
		;
	}
}