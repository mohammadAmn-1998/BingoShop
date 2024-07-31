using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Users.Domain.UserAgg;
using Users.Infrastructure.Context;
using Users.Infrastructure.Repositories;

namespace Users.Infrastructure.Bootstrapper
{
	public static class UsersInfraBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			services.AddTransient<IUserRepository, UserRepository>();
			services.AddTransient<IRoleRepository, RoleRepository>();
			services.AddTransient<IUserAddressRepository, UserAddressRepository>();

			services.AddDbContext<BlogUsers_Context>(options =>
			{
				options.UseSqlServer(connectionString);
			});
		}

		

	}
}
