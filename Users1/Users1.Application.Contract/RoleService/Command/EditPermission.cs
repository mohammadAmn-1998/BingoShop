using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Users1.Application.Contract.RoleService.Command
{
	public class EditPermission
	{
		

		public UserPermission UserPermission { get; set; }

	}
}
