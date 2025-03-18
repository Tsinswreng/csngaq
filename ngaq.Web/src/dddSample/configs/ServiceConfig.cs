using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ngaq.Core.dddSample.IF;
using ngaq.Infra.dddSample.email;

namespace ngaq.Web.dddSample.configs;

public static class ServiceConfig{
	public static IServiceCollection AddServiceConfig(
		this IServiceCollection svc
		,ILogger logger
		,WebApplicationBuilder builder
	){
		if(builder.Environment.IsDevelopment()){
			// Use a local test email server
			// See: https://ardalis.com/configuring-a-local-test-email-server/
			svc.AddScoped<I_sendEmailAsy, MimeKitEmailSender>();
			// Use a local test email server
			// See: https://ardalis.com/configuring-a-local-test-email-server/
		}else{
			svc.AddScoped<I_sendEmailAsy, MimeKitEmailSender>();
		}
		logger.LogInformation("{Project} services registered", "Mediatr and Email Sender");
		return svc;
	}
}