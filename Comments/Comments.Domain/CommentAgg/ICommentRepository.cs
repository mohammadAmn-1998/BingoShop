using Comments.Application.Contract.CommentService.Command;
using Shared.Domain.Enums;

namespace Comments.Domain.CommentAgg;

public interface ICommentRepository
{
	Task<bool> Create(CreateComment command);
	Task<bool> Reject(long commentId,string why);
	Task<bool> Approve(long commentId);

	long GetCommentsCountForUi(long ownerId, CommentFor commentFor);
}