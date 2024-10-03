using Comments.Application.Contract.CommentService.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Comments.Domain.CommentAgg;
using Shared.Domain.Enums;

namespace Comments.Application.Services
{
	internal class CommentService : ICommentService
	{
		private readonly ICommentRepository _commentRepository;

		public CommentService(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
		}

		public async Task<OperationResult> Create(CreateComment command)
			=>await _commentRepository.Create(command)
				? new(Status.Success)
				: new(Status.InternalServerError, ErrorMessages.InternalServerError);


		public async Task<bool> RejectCommentByAdmin(long commentId, string why)
			=> await _commentRepository.Reject(commentId, why);
			
		

		public async Task<bool> ApproveCommentByAdmin(long commentId)
		  => await _commentRepository.Approve(commentId);
	}
}
