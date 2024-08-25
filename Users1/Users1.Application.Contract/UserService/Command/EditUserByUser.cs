using Microsoft.AspNetCore.Http;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users1.Application.Contract.UserService.Command
{
	public class EditUserByUser
	{
		public long Id { get; set; }

		[Display(Name = "نام کامل")]
		[MaxLength(100, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string? FullName { get; set; }

		public string AvatarName { get; set; }
		[Display(Name = "تصویرکاربری")]
		public IFormFile? AvatarFile { get; set; }

		[Display(Name = "جنسیت")]
		public Gender UserGender { get; set; }

		[Display(Name = "بیوگرافی")]
		[MaxLength(1000, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string? biography { get; set; }
	}
}
