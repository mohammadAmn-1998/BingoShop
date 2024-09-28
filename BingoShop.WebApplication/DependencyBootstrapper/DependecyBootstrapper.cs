using BingoShop.WebApplication.Services;
using Blogs.Application.Bootstrapper;
using Blogs.Infrastructure.Bootstrapper;
using Blogs1.Application.Bootstrapper;
using Blogs1.Infrastructure.Bootstrapper;
using Blogs1.Query.Bootstrapper;
using Comments.Application.Bootstrapper;
using Comments.Infrastructure.Bootstrapper;
using Comments.Query.Bootstrapper;
using PostModule.Application.Services;
using PostModule.Infrastracture.EF;
using PostModule.Query;
using Seos.Application;
using Seos.Infrastructure;
using Seos.Query;
using Shared.Application.Services;
using Site.Application;
using Site.Infrastructure;
using Site.Query;
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
			CommentsApplicationBootstrapper.Config(services);
			CommentsQueryBootstrapper.Config(services);

			SeoInfrastructureBootstrapper.Config(services ,connectionString);
			SeoApplicationBootstrapper.Config(services);
			SeoQueryBootstrapper.Config(services);

			SiteApplicationBootstraper.Config(services);
			SiteInfrastructureBootstrapper.Config(services,connectionString);
			SiteQueryBootstrapper.Config(services);

			PostInfrastructureBootstrapper.Config(services,connectionString);
			PostApplicationBootstrapper.Config(services);
			PostQueryBootstrapper.Config(services);
		}

	}
}
