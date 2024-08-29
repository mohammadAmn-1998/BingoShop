using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Users1.Application.Contract.RoleService.Query
{
	public class PermissionQueryModel
	{
		public long Id { get; set; }

		public UserPermission UserPermission { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
