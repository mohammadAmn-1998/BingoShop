using Shared.Application;
using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Emails.Application.Contract.SendEmailApplication.Command
{
	public class CreateSendEmail
	{
		[Display(Name = "متن ایمیل")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public string Text { get; set; }
		[Display(Name = "عنوان ایمیل")]
		[MaxLength(250, ErrorMessage = ErrorMessages.MaxLengthError)]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		public string Title { get; set; }
	}
}
