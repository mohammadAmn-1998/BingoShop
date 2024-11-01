﻿using Shared.Domain.SeedWorks.Base;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Enums;

namespace Users.Domain.UserAgg
{
	public class Permission : BaseEntity<int>
	{

		public int RoleId { get; private set; }

		public UserPermission UserPermission { get; private set; }

		public Role Role { get; private set; }

		public Permission(int roleId, UserPermission userPermission)
		{
			RoleId = roleId;
			UserPermission = userPermission;
		}
	}
}
