using System.ComponentModel.DataAnnotations;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Query.Contract.Admin.Comment;

public class CommentAdminFilteredPaging : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public CommentStatus CommentStatus { get; set; }

	public CommentFor CommentFor { get; set; }

	public long? OwnerId { get; set; }

	public List<CommentAdminQueryModel>? Comments { get; set; }

}

