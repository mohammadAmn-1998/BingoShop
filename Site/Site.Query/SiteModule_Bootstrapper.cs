using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Site.Application;
using Site.Infrastructure;

namespace Site.Query
{
	public class SiteModule_Bootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			SiteInfrastructureBootstrapper.Config(services, connectionString);
			SiteApplicationBootstraper.Config(services);
			SiteQueryBootstrapper.Config(services);

		}

	}
}
