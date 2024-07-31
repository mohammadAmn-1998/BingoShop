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
	internal class UserAddressRepository : Repository<int,UserAddress> , IUserAddressRepository
	{
		public UserAddressRepository(BlogUsers_Context context) : base(context)
		{
		}
	}
}
