using Shared.Application.Services;
using Users1.Application.Bootstrapper;
using Users1.Infrastructure.Bootstrapper;
using Users1.Query.Bootstrapper;
using Users1.WebUI.Services;

namespace Users1.WebUI.Bootstrapper
{
	public static class DependencyBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			services.AddTransient<IFileService, FileService>();
			services.AddTransient<IAuthService, AuthService>();


			UserInfrastructureBootstrapper.Config(services, connectionString);
			UsersApplicationBootstrapper.Config(services);
			UsersQueryBootstrapper.Config(services);
		}

	}
}
