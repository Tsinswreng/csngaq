using Ardalis.ListStartupServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ngaq.Infra.dddSample.email;

namespace ngaq.Web.dddSample.configs;

public static class OptionConfig{
	public static IServiceCollection AddOptionConfig(
		this IServiceCollection svc
		,IConfiguration config
		,ILogger logger
		,WebApplicationBuilder builder
	){

		//# > 在appsettings.json
		/*
  "Mailserver": {
    "Server": "localhost",
    "Port": 25
  }
		*/
		svc.Configure<MailserverConfig>(config.GetSection("Mailserver"))
			// Configure Web Behavior
			.Configure<CookiePolicyOptions>(opt=>{
				opt.CheckConsentNeeded = ctx=>true; //强制用户必须同意 Cookie 使用（常用于 GDPR 合规）。
				opt.MinimumSameSitePolicy = SameSiteMode.None; // 允许跨站请求携带 Cookie。
			})
		;//?

		if(builder.Environment.IsDevelopment()){
			//仅在开发环境下启用服务列表展示功能（来自 Ardalis.ListStartupServices 包
			svc.Configure<Ardalis.ListStartupServices.ServiceConfig>(cfg=>{
				cfg.Services = new List<ServiceDescriptor>(builder.Services);//?
				cfg.Path = "list/services";
			});
		}

		logger.LogInformation("{Project} were configured", "Options");
		return svc;
	}
}