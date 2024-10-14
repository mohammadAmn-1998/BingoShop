using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Admin.Comment;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Query.Services.Services
{
	internal class CommentAdminQuery : BaseRepository, ICommentAdminQuery
	{

		public CommentAdminQuery(CommentContext context) : base(context)
		{
			
		}

		public async Task<CommentAdminFilteredPaging>  GetCommentsForAdmin(FilterParams filterParams, CommentStatus status , CommentFor commentFor ,long? ownerId,long parentId )
		{
			try
			{

				var result = Table<Comment>().Include(x => x.ChildComments).Include(c => c.ParentComment).OrderByDescending(x=> x.Status == CommentStatus.هنوز_دیده_نشده).AsQueryable();

				if (ownerId != null)
				{
					result = result.Where(x => x.OwnerId == ownerId);
				}

				if (  parentId > 0)
				{
					result = result.Where(x => x.ParentId == parentId);
				}
				else if (parentId == 0)
				{
					result = result.Where(x => x.ParentId == null);
				}

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					result = result.Where(x =>
						x.FullName.ToLower().Contains(filterParams.Title.ToLower()) ||
						 (x.Email != null && x.Email.ToLower().Contains(filterParams.Title.ToLower())) ||
						 x.Text.ToLower().Contains(filterParams.Title.ToLower()));
				}

				

				if (status != CommentStatus.همه)
				{
					result = result.Where(x => x.Status == status);
				}

				if (commentFor != CommentFor.همه)
				{
					result = result.Where(x => x.CommentFor == commentFor);
				}

				CommentAdminFilteredPaging model = new();
				model.GetBasePagination(result,filterParams.PageId,filterParams.Take);
				model.FilterParams = filterParams;
				model.CommentStatus = status;
				model.CommentFor = commentFor;
				model.OwnerId = ownerId;
				model.Comments = new();

				if (result.Count() > 0)
				{
					model.Comments = await result.Skip(model.Skip).Take(model.Take).Select(x =>
						new CommentAdminQueryModel
						{
							Id = x.Id,
							OwnerId = x.OwnerId,
							UserId = x.UserId,
							CommentStatus = x.Status,
							CommentFor = x.CommentFor,
							FullName = x.FullName,
							Email = x.Email,
							Text = x.Text,
							WhyRejected = x.WhyRejected,
							CreateDate = x.CreateDate.ConvertToPersianDate(),
							ParentId = x.ParentId,
							ParentComment = x.ParentComment !=null ? new CommentAdminQueryModel
							{
								Id = x.ParentComment.Id,
								Text = x.ParentComment.Text,
								
							}: null,
							ChildComments = x.ChildComments.Select(s => new CommentAdminQueryModel
								{
									Id = x.Id,
									FullName = x.FullName,
									Text = x.Text,

								})
								.ToList()
						}).ToListAsync();

				}

				return model;
			}
			catch (Exception e)
			{
				return new();
			}
		}

		public async Task<CommentAdminQueryModel?> GetCommentDetailForAdmin(long id)
		{
			try
			{
				var result = await GetById<Comment>(id);

				if (result == null)
					throw new NullReferenceException();

				return new()
				{
					Id = result.Id,
					OwnerId = result.OwnerId,
					UserId = result.UserId,
					CommentStatus = result.Status,
					CommentFor = result.CommentFor,
					FullName = result.FullName,
					Email = result.Email,
					Text = result.Text,
					WhyRejected = result.WhyRejected,
					ParentId = result.ParentId,
					CreateDate = result.CreateDate.ConvertToPersianDate(),
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
