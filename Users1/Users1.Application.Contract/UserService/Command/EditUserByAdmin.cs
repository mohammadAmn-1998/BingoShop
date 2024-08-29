using Microsoft.AspNetCore.Http;
using Shared.Application.Utility.Validations;
using Shared.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Users1.Application.Contract.UserService.Command
{
	public class EditUserByAdmin : EditUserByUser
	{

		
		[Display(Name = "شماره همراه")]
		[MobileValidation(ErrorMessage = ErrorMessages.MobileIsInvalid)]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public string Mobile { get; set; }

		[Display(Name = "ایمیل")]
		[EmailValidation(ErrorMessage = ErrorMessages.EmailIsInvalid)]
		[MaxLength(200, ErrorMessage = ErrorMessages.MaxLengthError)]
		public string? Email { get; set; }

		public bool IsActive { get; set; }
		public bool IsBanned { get; set; }

	}
}
