using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Users1.Domain.UserAgg
{
	public class Permission : BaseEntity<long>
	{

		public long RoleId { get; private set; }
		public UserPermission UserPermission { get; private set; }
		public Role Role { get; private set; }
		public Permission(long roleId, UserPermission userPermission)
		{
			RoleId = roleId;
			UserPermission = userPermission;
		}

	}
}
