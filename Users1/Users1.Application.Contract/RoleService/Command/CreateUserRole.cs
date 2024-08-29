using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users1.Application.Contract.RoleService.Query;

namespace Users1.Application.Contract.RoleService.Command
{
	public class CreateUserRole
	{


		public long userId { get; set; }

		public List<RoleQueryModel>? Roles { get; set; }

		public long roleId { get; set; }


	}
}
