using Emails.Application.Bootstrapper;
using Emails.Infrastructure.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Emails.Query.Bootstrapper
{
	public class EmailsModule_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			EmailInfrastructureBootstrapper.Config(services, connectionString);
			EmailApplicationBootstrapper.Config(services);
			EmailQueryBootstrapper.Config(services);

		}
	}
}
