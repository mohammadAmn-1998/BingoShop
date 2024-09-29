using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Users1.Application.Bootstrapper;
using Users1.Infrastructure.Bootstrapper;

namespace Users1.Query.Bootstrapper
{
	public class UsersModule_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			UserInfrastructureBootstrapper.Config(services, connectionString);
			UsersApplicationBootstrapper.Config(services);
			UsersQueryBootstrapper.Config(services);

		}

	}
}
