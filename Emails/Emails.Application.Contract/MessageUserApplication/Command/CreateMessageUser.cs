using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;

namespace Emails.Application.Contract.MessageUserApplication.Command
{
    public class CreateMessageUser
    {
        public long UserId { get; set; }

		[Display(Name = "نام کامل")]
		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        public string FullName { get; set; }

		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
        [Display(Name = " عنوان")]
        public string Subject { get; set; }
		
		[Display(Name = "شماره تماس ")]
		public string? PhoneNumber { get; set; }
				
		[Display(Name = "ایمیل  ")]
		public string? Email { get; set; }

		[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
		[Display(Name = "متن ")]
		public string Message { get; set; }
    }
}
