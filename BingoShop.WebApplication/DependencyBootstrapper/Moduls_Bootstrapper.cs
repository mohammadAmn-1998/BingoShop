using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Users.Application.Bootstrapper;
using Users.Infrastructure.Bootstrapper;

namespace BingoShop.WebApplication.DependencyBootstrapper
{
	public class Moduls_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			BlogInfraBootstrapper.Config(services,connectionString);
			BlogServiceBootstrapper.Config(services);

			UsersInfraBootstrapper.Config(services,connectionString);
			UserServicesBootstrapper.Config(services);

		}

	}
}
