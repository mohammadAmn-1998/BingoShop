using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Comments.Application.Contract.CommentService.Command
{
	public interface ICommentService
	{

		Task<OperationResult> Create(CreateComment command);

		Task<bool> RejectCommentByAdmin(long commentId, string why);

		Task<bool> ApproveCommentByAdmin(long commentId);

	}

	public class CreateComment
	{

		public string UserFullName { get; set; }

		public long UserId { get; set; }

		public string? Email { get; set; }

		public string Text { get; set; }

		public long ParentId { get; set; }

		public long OwnerId { get; set; }

		public CommentFor CommentFor { get; set; }

	}
}
