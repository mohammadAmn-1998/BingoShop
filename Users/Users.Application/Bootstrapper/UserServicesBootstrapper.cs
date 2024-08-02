using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Users.Application.Services.Implements;
using Users.Application.Services.Interfaces;

namespace Users.Application.Bootstrapper
{
	public static class UserServicesBootstrapper
	{

		public static void Config(IServiceCollection services)
		{

			services.AddTransient<IUserService, UserService>();
			services.AddTransient<IUserAddressService, UserAddressService>();
			services.AddTransient<IRoleService, RoleService>();

		}

	}
}
