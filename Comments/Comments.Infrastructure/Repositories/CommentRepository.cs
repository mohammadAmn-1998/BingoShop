using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Application.Contract.CommentService.Command;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;

namespace Comments.Infrastructure.Repositories
{
	internal class CommentRepository : BaseRepository, ICommentRepository
	{
		public CommentRepository(CommentContext context) : base(context)
		{
		}

		public async Task<bool> Create(CreateComment command)
		{
			try
			{

				var comment = new Comment(command.UserId, command.OwnerId, command.UserFullName, command.Email,
					command.Text, command.CommentFor, command.ParentId);

				Insert(comment);
				return await Save() > 0;

			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> Reject(long commentId, string why)
		{
			try
			{
				var comment = await GetById<Comment>(commentId);
				if (comment == null)
					throw new NullReferenceException();

				comment.RejectedComment(why);
				Update(comment);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}

		public async Task<bool> Approve(long commentId)
		{
			try
			{
				var comment = await GetById<Comment>(commentId);
				if (comment == null)
					throw new NullReferenceException();

				comment.AcceptedComment();
				Update(comment);
				return await Save() > 0;
			}
			catch (Exception e)
			{
				return false;
			}
		}
	}
}
