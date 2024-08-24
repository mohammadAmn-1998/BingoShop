using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Dtos.RoleDtos;
using Users.Application.Dtos.UserDtos;

namespace Users.Application.Dtos.UserRoleDtos
{
	public class UserRoleDto
	{

		public int Id { get; set; }

		public int RoleId { get; set; }

		public int UserId { get; set; }

		public UserDto User { get; set; }

		public RoleDto Role { get; set; }
	}
}
