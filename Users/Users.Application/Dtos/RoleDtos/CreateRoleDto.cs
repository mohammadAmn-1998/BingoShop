using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Users.Application.Dtos.RoleDtos
{
	public class CreateRoleDto
	{

		public int RoleId { get; set; }

		[Display(Name = "عنوان نقش")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public string Title { get; set; }

	}
}
