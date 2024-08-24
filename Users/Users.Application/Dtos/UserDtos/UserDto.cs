using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.UserAgg;

namespace Users.Application.Dtos.UserDtos
{
	public class UserDto
	{

		public int Id { get; set; }

		public string UserName { get; set; }

		public string Avatar { get; set; }

		public string Email { get; set; }

		public bool IsActive { get; set; }	

	}
}
