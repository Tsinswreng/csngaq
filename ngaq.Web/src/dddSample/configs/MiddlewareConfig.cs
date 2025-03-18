using Ardalis.ListStartupServices;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ngaq.Infra.dddSample.data;

namespace ngaq.Web.dddSample.configs;

public static class MiddlewareConfig{
	public static async Task<IApplicationBuilder> useAppMiddlewareAndSeedDatabase(
		this WebApplication app
	){
		if(app.Environment.IsDevelopment()){
			app.UseDeveloperExceptionPage();//显示详细的错误页面，便于调试。
			//>来自Ardalis库，用于列出所有注册的服务，方便开发时查看依赖关系
			app.UseShowAllServicesMiddleware();//? // see https://github.com/ardalis/AspNetCoreStartupServices
			return app;
		}
		app.UseDefaultExceptionHandler(); // from FastEndpoints
		app.UseHsts();//強制用Https

		app.UseFastEndpoints()
			.UseSwaggerGen() // Includes AddFileServer and static files middleware
		;

		//>把http請求緟定向到https
		app.UseHttpsRedirection(); // Note this will drop Authorization headers

		await SeedDatabase(app);
		return app;

	}

	static async Task SeedDatabase(WebApplication app){
		using var scope = app.Services.CreateScope();//?
		var services = scope.ServiceProvider;
		try{
			var context = services.GetRequiredService<AppDbCtx>();
			//context.Database.Migrate();
			context.Database.EnsureCreated();//?
			await SeedData.InitAsy(context);

		}
		catch (System.Exception ex){
			var logger = services.GetRequiredService<ILogger<Program>>();
			logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
		}
	}
}