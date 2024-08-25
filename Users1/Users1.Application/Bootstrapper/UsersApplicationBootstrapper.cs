using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Users1.Application.Contract.RoleService.Command;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Services;

namespace Users1.Application.Bootstrapper
{
	public static class UsersApplicationBootstrapper
	{

		public static void Config(IServiceCollection services)
		{

			services.AddTransient<IUserService,UserService>();
			services.AddTransient<IRoleService,RoleService>();

		}

	}
}
