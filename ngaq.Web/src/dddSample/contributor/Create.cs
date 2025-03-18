using FastEndpoints;
using MediatR;
using ngaq.UseCases.dddSample.contributor.create;

namespace ngaq.Web.dddSample.contributor;

public class Create(IMediator _mediator)
	:Endpoint<
		Req_CreateContributor
		,Res_CreateContributor
	>
{
	public override void Configure() {
		//base.Configure();
		Post(Req_CreateContributor.Route);
		AllowAnonymous();//
		Summary(s=>{
		// XML Docs are used by default but are overridden by these properties:
		//s.Summary = "Create a new Contributor.";
		//s.Description = "Create a new Contributor. A valid name is required.";
			s.ExampleRequest = new Req_CreateContributor{name="Contributor Name"};
		});//?
	}
	public override async Task HandleAsync(Req_CreateContributor req, CancellationToken ct) {
		var result = await _mediator.Send(new CreateCmd_Contributor(req.name!, req.phoneNumber));
		if(result.IsSuccess){
			Response = new Res_CreateContributor(result.Value, req.name!);
			return;
		}
	}
}