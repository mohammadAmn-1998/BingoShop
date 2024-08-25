using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Users1.Domain.UserAgg
{
	public class UserRole : BaseEntity<long>
	{

		public long UserId { get; private set; }
		public long RoleId { get; private set; }

		public Role Role { get; private set; }
		public User User { get; private set; }

		public UserRole(long userId, long roleId)
		{
			UserId = userId;
			RoleId = roleId;
		}

	}
}
