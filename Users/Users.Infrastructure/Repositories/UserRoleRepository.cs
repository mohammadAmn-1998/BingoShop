using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Infrastructure.BaseRepository;
using Users.Domain.UserAgg;
using Users.Infrastructure.Context;
using Exception = System.Exception;

namespace Users.Infrastructure.Repositories
{
	internal class UserRoleRepository : Repository<int,UserRole> , IUserRoleRepository
	{
		public UserRoleRepository(BlogUsers_Context context) : base(context)
		{
		}


		public List<UserRole>? GetUserRoles(int userId)
		{


			try
			{

				var userRoles = GetAsQueryable(x => x.UserId == userId, true);

				return userRoles?.ToList();


			}
			catch (Exception e)
			{
				return null;
			}

		}

	}
}
