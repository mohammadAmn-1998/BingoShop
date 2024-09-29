using Shared.Domain.Enums;

namespace Query.Contract.Admin.Comment;

public class CommentAdminQueryModel
{
	public long Id { get; set; }

	public long UserId { get; set; }

	public long OwnerId { get; set; }

	public string FullName { get; set; }

	public string? Email { get; set; }

	public string Text { get; set; }

	public string? WhyRejected { get; set; }

	public long ParentId { get; set; }

	public CommentStatus CommentStatus { get; set; }

	public CommentFor CommentFor { get; set; }

	public List<CommentAdminQueryModel>? ChildComments { get; set; }

	public CommentAdminQueryModel? ParentComment { get; set; }

}