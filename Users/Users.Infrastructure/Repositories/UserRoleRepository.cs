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
		private readonly BlogUsers_Context _context;

		public UserRoleRepository(BlogUsers_Context context) : base(context)
		{
			_context = context;
		}


		public List<UserRole>? GetUserRoles(int userId)
		{


			try
			{

				var userRoles = _context.UserRoles.Where(x => x.UserId == userId).Include(x => x.User)
					.Include(x => x.Role).ThenInclude(x => x.Permissions);

				return userRoles?.ToList();


			}
			catch (Exception e)
			{
				return null;
			}

		}

	}
}
