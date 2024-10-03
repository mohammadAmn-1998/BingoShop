using System.ComponentModel.DataAnnotations;
using Shared.Domain.Enums;

namespace Query.Contract.Admin.MessageUser;

public class MessageUserAdminQueryModel
{
	public long Id { get; set; }

	[Display(Name = "تاریخ پیام")]
	public string CreateDate { get; set; }

	[Display(Name = "آی دی کاربر")]
	public long UserId { get;  set; }

	[Display(Name = "وضعیت")]
	public MessageStatus Status { get;  set; }

	[Display(Name = "نام کامل")]
	public string UserName { get; set; }

	[Display(Name = "عنوان")]
	public string Subject { get; set; }

	[Display(Name = "شماره تماس")]
	public string? PhoneNumber { get; set; }

	[Display(Name = "ایمیل")]
	public string? Email { get; set; }

	[Display(Name = "متن")]
	public string Message { get; set; }

	[Display(Name = "جواب")]
	public string? AnswerSms { get; set; }

	[Display(Name = "جواب")]
	public string? AnswerEmail { get; set; }

}