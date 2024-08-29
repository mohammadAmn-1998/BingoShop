using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Users1.Application.Contract.RoleService.Query;
using Users1.Application.Contract.UserService.Query;
using Users1.Query.Services;

namespace Users1.Query.Bootstrapper
{
	public static class UsersQueryBootstrapper
	{


		public static void Config(IServiceCollection services)
		{

			services.AddTransient<IUserQuery, UserQuery>();
			services.AddTransient<IRoleQuery, RoleQuery>();

		}
	}
}
