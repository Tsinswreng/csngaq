using Microsoft.AspNetCore.Builder;
using Serilog;

namespace ngaq.Web.dddSample.configs;


public static class LoggerConfig{
	public static WebApplicationBuilder AddLoggerConfigs(
		this WebApplicationBuilder b
	){
		b.Host.UseSerilog((_,config)=>{
			config.ReadFrom.Configuration(b.Configuration);
		});
		return b;
	}
}