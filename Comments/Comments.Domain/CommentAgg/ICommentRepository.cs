using Comments.Application.Contract.CommentService.Command;

namespace Comments.Domain.CommentAgg;

public interface ICommentRepository
{
	Task<bool> Create(CreateComment command);
	Task<bool> Reject(long commentId,string why);
	Task<bool> Approve(long commentId);
}