using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users1.Domain.UserAgg
{
	public class UserRole
	{

		public int UserId { get; private set; }
		public int RoleId { get; private set; }

		public Role Role { get; private set; }
		public User User { get; private set; }

		public UserRole(int userId, int roleId)
		{
			UserId = userId;
			RoleId = roleId;
		}

	}
}
