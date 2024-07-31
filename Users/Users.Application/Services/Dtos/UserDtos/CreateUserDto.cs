using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Application.Utility.Validations;

namespace Users.Application.Services.Dtos.UserDtos
{
	public class CreateUserDto
	{
		[Display(Name = "شماره موبایل")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[MobileValidation(ErrorMessage = ErrorMessages.MobileIsInvalid)]
		public string Mobile { get; set; }

		[Display(Name = "پسورد")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[Length(5,8,ErrorMessage = ErrorMessages.PasswordLengthError)]
		[PasswordValidation]
		public string Password { get; set; }

		[Display(Name = " تکرار پسورد")]
		[Compare(nameof(Password),ErrorMessage = ErrorMessages.PasswordConfirmError)]
		public string ConfirmPassword { get; set; }


	}
}
