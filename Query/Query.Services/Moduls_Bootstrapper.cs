using Blogs1.Query.Bootstrapper;
using Comments.Query.Bootstrapper;
using Emails.Query.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using PostModule.Query;
using Seos.Query;
using Site.Query;
using Users1.Query.Bootstrapper;

namespace Query.Services
{
	public class Moduls_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{
			Blogs1Module_Bootstrapper.Config(services, connectionString);
			SiteModule_Bootstrapper.Config(services, connectionString);
			PostModule_Bootstrapper.Config(services,connectionString);
			UsersModule_Bootstrapper.Config(services,connectionString);
			CommentsModule_Bootstrapper.Config(services,connectionString);
			SeoModule_Bootstrapper.Config(services,connectionString);
			EmailsModule_Bootstrapper.Config(services,connectionString);

		}

	}
}
