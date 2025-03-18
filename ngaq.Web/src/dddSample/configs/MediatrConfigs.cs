using System.Reflection;
using Ardalis.SharedKernel;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ngaq.Core.dddSample.contributorAgg;
using ngaq.UseCases.dddSample.contributor.create;

namespace ngaq.Web.dddSample.configs;

public static class MediatrConfigs{
	public static IServiceCollection AddMediatrConfigs(
		this IServiceCollection s
	){
		var mediatRAssemblys = new []{
			Assembly.GetAssembly(typeof(Contributor))//Core
			,Assembly.GetAssembly(typeof(CreateCmd_Contributor))//UseCases
		};

		s.AddMediatR(c=>c.RegisterServicesFromAssemblies(mediatRAssemblys!))
			.AddScoped(
				typeof(IPipelineBehavior<,>)
				,typeof(LoggingBehavior<,>)
			)
			.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>()
		;
		return s;
	}
}