using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogService.Query;
using Blogs1.Domain.BlogAgg;
using Blogs1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Models;
using Shared.Domain.SeedWorks.Base;

namespace Blogs1.Query.Services
{
	internal class BlogQuery : BaseRepository , IBlogQuery
	{
		public BlogQuery(BlogContext context) : base(context)
		{
		}

		public async Task<FilteredBlogQueryModel?> GetFilteredPosts(FilterParams filterParams)
		{
			try
			{
				var result = Table<Blog>().OrderByDescending(x=> x.CreateDate).AsQueryable();

				if (!string.IsNullOrEmpty(filterParams.Title))
				{

					result = result.Where(x => x.Title.Contains(filterParams.Title.Trim()));

				}

				FilteredBlogQueryModel model = new();
				model.GetBasePagination(result,filterParams.PageId,filterParams.Take);

				var skip = (filterParams.PageId - 1) * filterParams.Take;

				var blogs = await result.Skip(skip).Take(filterParams.Take).Select(x =>
					new BlogQueryModel
					{
						Id = x.Id,
						Title = x.Title,
						ImageName = x.ImageName,
						ImageAlt = x.ImageAlt,
						UserId = x.UserId,
						Author = x.Author,
						CategoryId = x.CategoryId,
						SubCategoryId = x.SubCategoryId,
						CategoryTitle = Table<BlogCategory>().First(c=> c.Id == x.CategoryId).Title,
						SubCategoryTitle = Table<BlogCategory>().First(c=> c.Id == x.SubCategoryId).Title,
						Slug = x.Slug,
						Summary = x.Summary,
						Content = x.Content,
						TotalVisits = x.TotalVisits,
						IsSpecial = x.IsSpecial,
						Likes = x.Likes,
						Dislikes = x.Dislikes,
						CreateDate = x.CreateDate,
						UpdateDate = x.UpdateDate,
						Active = x.Active
					}).ToListAsync();

				model.Blogs = blogs;
				model.FilterParams = filterParams;

				return model;

			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
