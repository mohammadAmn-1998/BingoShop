using Microsoft.Extensions.DependencyInjection;
using Seos.Application;
using Seos.Infrastructure;
using Seos.Query;

namespace Seos.Query
{
	public class SeoModule_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			SeoInfrastructureBootstrapper.Config(services, connectionString);
			SeoApplicationBootstrapper.Config(services);
			SeoQueryBootstrapper.Config(services);

		}

	}
}
