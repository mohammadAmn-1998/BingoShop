using Comments.Application.Bootstrapper;
using Comments.Infrastructure.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Query.Bootstrapper
{
	public class CommentsModule_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			CommentsInfrastructureBootstrapper.Config(services, connectionString);
			CommentsApplicationBootstrapper.Config(services);
			CommentsQueryBootstrapper.Config(services);

		}
	}
}
