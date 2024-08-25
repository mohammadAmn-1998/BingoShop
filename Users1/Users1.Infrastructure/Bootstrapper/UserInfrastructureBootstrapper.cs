using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users1.Domain.UserAgg.IRepositories;
using Users1.Infrastructure.Context;
using Users1.Infrastructure.Repositories;

namespace Users1.Infrastructure.Bootstrapper
{
	public  static class UserInfrastructureBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{


			services.AddDbContext<UsersContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IRoleRepository, RoleRepository>();

		}

	}
}
