using System.Reflection.Metadata.Ecma335;
using Shared.Application.Models;
using Shared.Domain.Enums;

namespace Query.Contract.Admin.Comment;

public interface ICommentAdminQuery
{
	Task<CommentAdminFilteredPaging> GetCommentsForAdmin(FilterParams  filterParams,CommentStatus status  , CommentFor commentFor  );

	Task<CommentAdminQueryModel?> GetCommentDetailForAdmin(long id);
}