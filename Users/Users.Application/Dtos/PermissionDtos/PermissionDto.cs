using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Users.Application.Dtos.PermissionDtos
{
	public class PermissionDto
	{

		public int Id { get; set; }

		public int RoleId { get; set; }

		public UserPermission UserPermission { get; set; }



	}
}
