using Blogs1.Application.Bootstrapper;
using Blogs1.Infrastructure.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs1.Query.Bootstrapper
{
	public class Blogs1Module_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			Blogs1InfrastructureBootstrapper.Config(services, connectionString);
			Blogs1ApplicationBootstrapper.Config(services);
			Blogs1QueryBootstrapper.Config(services);

		}
	}
}
