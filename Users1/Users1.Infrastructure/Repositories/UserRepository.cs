using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;
using Users1.Domain.UserAgg.IRepositories;
using Users1.Infrastructure.Context;

namespace Users1.Infrastructure.Repositories
{
	internal class UserRepository : BaseRepository , IUserRepository
	{
		public UserRepository(UsersContext context) : base(context)
		{
		}
	}
}
