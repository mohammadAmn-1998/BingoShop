using BingoShop.WebApplication.Services;
using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Blogs1.Application.Bootstrapper;
using Blogs1.Infrastructure.Bootstrapper;
using Blogs1.Query.Bootstrapper;
using Comments.Infrastructure.Bootstrapper;
using Seos.Infrastructure;
using Shared.Application.Services;
using Users1.Application.Bootstrapper;
using Users1.Infrastructure.Bootstrapper;
using Users1.Query.Bootstrapper;


namespace BingoShop.WebApplication.DependencyBootstrapper
{
	public class DependencyBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{



			services.AddTransient<IFileService, FileService>();
			services.AddTransient<IAuthService, AuthService>();

			UserInfrastructureBootstrapper.Config(services, connectionString);
			UsersApplicationBootstrapper.Config(services);
			UsersQueryBootstrapper.Config(services);


			Blog1InfraBootstrapper.Config(services,connectionString);
			Blog1ApplicationBootstrapper.Config(services);
			Blog1QueryBootstrapper.Config(services);


			CommentsInfrastructureBootstrapper.Config(services, connectionString);
			
			SeoInfrastructureBootstrapper.Config(services ,connectionString);


		}

	}
}
