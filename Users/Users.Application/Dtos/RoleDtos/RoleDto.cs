using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos.PermissionDtos;
using Users.Application.Dtos.UserRoleDtos;

namespace Users.Application.Dtos.RoleDtos
{
	public class RoleDto
	{

		public int RoleId { get; set; }

		public string Title { get; set; }

		public List<PermissionDto> PermissionDtos { get; set; }

		 public List<UserRoleDto> UserRoleDtos { get; set; }


	}
}
