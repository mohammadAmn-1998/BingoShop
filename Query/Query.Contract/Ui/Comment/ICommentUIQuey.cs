using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Query.Contract.Ui.Comment;

public interface ICommentUIQuey
{

	Task<CommentUIPaging> GetCommentsForUI(long ownerId, CommentFor commentFor,int pageId);

}


public class CommentUIPaging : BasePagination
{


	public int PageId { get; set; }

	public long OwnerId { get; set; }

	public long CommentsCount { get; set; }

	public CommentFor CommentFor { get; set; }

	public List<CommentUIQueryModel> Comments { get; set; }

}