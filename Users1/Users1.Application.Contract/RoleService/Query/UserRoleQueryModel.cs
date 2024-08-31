using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users1.Application.Contract.UserService.Query;

namespace Users1.Application.Contract.RoleService.Query
{
	public class UserRoleQueryModel
	{
		public long Id { get; set; }

		public long UserId { get; set; }

		public long RoleId { get; set; }

		public RoleQueryModel Role { get; set; }

		public UserQueryModel User { get; set; }

	}
}
