using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Query.Contract.Ui.Comment;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Users1.Domain.UserAgg.IRepositories;

namespace Query.Services.Services
{
	internal class CommentUIQuery : BaseRepository, ICommentUIQuey
	{
		private readonly IUserRepository _userRepository;

		

		public CommentUIQuery(CommentContext context, IUserRepository userRepository) : base(context)
		{
			_userRepository = userRepository;
		}


		public async Task<CommentUIPaging> GetCommentsForUI(long ownerId, CommentFor commentFor, int pageId)
		{
			try
			{

				var result = Table<Comment>().Where(x =>
					(x.Status == CommentStatus.قبول_شده || x.Status == CommentStatus.هنوز_دیده_نشده) &&
					x.OwnerId == ownerId && x.CommentFor == commentFor);


				CommentUIPaging model = new();

				model.CommentFor = commentFor;
				model.CommentsCount = result.Count();
				model.OwnerId = ownerId;
				model.PageId = pageId;
				model.GetBasePagination(result,pageId,6);
				model.Comments = new();
				if (result.Any())
					model.Comments = await result.Where(x => x.ParentId == null).Skip(model.Skip).Take(model.Take).Select(
						x => new CommentUIQueryModel
						{
							Id = x.Id,
							FullName = x.FullName,
							Text = x.Text,
							Avatar = x.UserId > 0 ? _userRepository.GetById(x.UserId)!.Avatar : "Default.png",
							CreationDate = x.CreateDate.ConvertToPersianDate(),
							ParentId = x.ParentId,
							Childs = result.Where(s => s.ParentId == x.Id).Select(s => new CommentUIQueryModel
							{
								Id = s.Id,
								FullName = s.FullName,
								Text = s.Text,
								Avatar = s.UserId > 0 ? _userRepository.GetById(s.UserId)!.Avatar : "Default.png",
								CreationDate = s.CreateDate.ConvertToPersianDate(),
								ParentId = s.ParentId,
								Childs = new()
							}).ToList()
						}
					).ToListAsync();


				return model;
			}
			catch (Exception e)
			{
				return new();
			}
		}
	}
}

public static class CommentMapper 
{

	public static CommentUIQueryModel MapCommentToCommentUIQueryModel(Comment comment,IQueryable<Comment> result,IUserRepository _userRepository)
	{

		return new()
		{
			FullName = comment.FullName,
			Text = comment.Text,
			Avatar = comment.UserId > 0 ? _userRepository.GetById(comment.UserId)!.Avatar : "Default.png" +
				"" ,
			CreationDate = comment.CreateDate.ConvertToPersianDate(),
			ParentId = comment.ParentId,
			Childs = result.Where(x=> x.ParentId == comment.Id).Select(s=> MapCommentToCommentUIQueryModel(s, result,_userRepository)).ToList()
		};

	}

}
