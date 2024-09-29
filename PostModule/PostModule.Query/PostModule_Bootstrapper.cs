using Microsoft.Extensions.DependencyInjection;
using PostModule.Application.Services;
using PostModule.Infrastracture.EF;

namespace PostModule.Query
{
	public class PostModule_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			PostInfrastructureBootstrapper.Config(services, connectionString);
			PostApplicationBootstrapper.Config(services);
			PostQueryBootstrapper.Config(services);

		}

	}
}
