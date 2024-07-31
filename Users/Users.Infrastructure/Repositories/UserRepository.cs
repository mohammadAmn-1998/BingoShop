using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.BaseRepository;
using Users.Domain.UserAgg;
using Users.Infrastructure.Context;

namespace Users.Infrastructure.Repositories
{
	internal class UserRepository : Repository<int,User> , IUserRepository
	{
		public UserRepository(BlogUsers_Context context) : base(context)
		{
		}
	}
}
