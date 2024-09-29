using System.ComponentModel.DataAnnotations;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Comments.Application.Contract.CommentService.Command;

public class ChangeCommentStatus
{
	public long Id { get; set; }

	[Display(Name = "وضعیت کامنت")]
	public CommentStatus CommentStatus { get; set; }

	[Display(Name = "دلیل رد شدن کامنت")]
	[MaxLength(3000,ErrorMessage = ErrorMessages.MaxLengthError)]
	public string? WhyRejected { get; set; }
}