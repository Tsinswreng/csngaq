using Microsoft.Extensions.DependencyInjection;
using ngaq.Core.Svc.Crud.WordCrud.IF;
using ngaq.Server.Svc.Crud.WordCrud;
namespace ngaq.Desktop;

public class Setup{
	public zero ConfigureServices(
		IServiceCollection services
	){
		services.AddTransient<I_SeekFullWordKVByIdAsy, WordSeeker>();
		return 0;
	}
}