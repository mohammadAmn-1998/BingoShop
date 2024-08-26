using Shared.Application.Utility.Validations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Users1.Application.Contract.UserService.Command
{
	public class LoginUser
	{

		[Display(Name = "شماره همراه")]
		[MobileValidation(ErrorMessage = ErrorMessages.MobileIsInvalid)]
		public string Mobile { get; set; }

		[Display(Name = "رمز شش رقمی")]
		[MaxLength(6,ErrorMessage = ErrorMessages.MaxLengthError)]
		[MinLength(6,ErrorMessage = ErrorMessages.MinLengthError)]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public string PassKey { get; set; }

		public string ReturnUrl { get; set; }
	}
}
