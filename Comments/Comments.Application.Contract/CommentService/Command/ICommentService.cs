using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Comments.Application.Contract.CommentService.Command
{
	public interface ICommentService
	{

		Task<OperationResult> Create(CreateComment command);

		Task<bool> RejectCommentByAdmin(long commentId, string why);

		Task<bool> ApproveCommentByAdmin(long commentId);


	}
}
