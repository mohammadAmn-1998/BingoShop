using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Comments.Application.Contract.CommentService.Command;

public class CreateComment
{

	[Display(Name = "نام شما")]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	[MaxLength(100,ErrorMessage = ErrorMessages.MaxLengthError)]
	public string UserFullName { get; set; }

	public long UserId { get; set; }

	[Display(Name = "ایمیل (اختیاری)")]
	[MaxLength(200,ErrorMessage = ErrorMessages.MaxLengthError)]
	public string? Email { get; set; }

	[Display(Name = "متن")]
	[Required(ErrorMessage = ErrorMessages.FieldIsRequired)]
	[MaxLength(3000,ErrorMessage = ErrorMessages.MaxLengthError)]
	public string Text { get; set; }

	public long ParentId { get; set; }

	public long OwnerId { get; set; }

	public CommentFor CommentFor { get; set; }

}