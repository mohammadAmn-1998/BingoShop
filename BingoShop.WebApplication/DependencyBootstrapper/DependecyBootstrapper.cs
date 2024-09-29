using BingoShop.WebApplication.Services;
using Blogs1.Application.Bootstrapper;
using Blogs1.Infrastructure.Bootstrapper;
using Blogs1.Query.Bootstrapper;
using Comments.Application.Bootstrapper;
using Comments.Infrastructure.Bootstrapper;
using Comments.Query.Bootstrapper;
using Emails.Application.Bootstrapper;
using Emails.Infrastructure.Bootstrapper;
using Emails.Query.Bootstrapper;
using PostModule.Application.Services;
using PostModule.Infrastracture.EF;
using PostModule.Query;
using Query.Services;
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
			Moduls_Bootstrapper.Config(services, connectionString);
			

		}

	}
}
