using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users1.Application.Contract.UserService.Query;

namespace Users1.Application.Contract.RoleService.Query
{
	public class RoleQueryModel
	{

		public long Id { get; set; }

		public string Title { get; set; }

		public List<PermissionQueryModel> Permissions { get; set; }

		public List<UserQueryModel> Users { get; set; }

		public DateTime CreateDate { get; set; }

		public DateTime? UpdateDate { get; set; }


	}
}
