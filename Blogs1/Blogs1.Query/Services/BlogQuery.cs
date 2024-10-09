using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Blogs1.Application.Contract.BlogService.Query;
using Blogs1.Domain.BlogAgg;
using Blogs1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Application.Models;
using Shared.Application.Utility;
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

				

				var blogs = await result.Skip(model.Skip).Take(filterParams.Take).Select(x =>
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
						CreateDate = x.CreateDate.ConvertToPersianDate(),
						UpdateDate = x.UpdateDate.ConvertToPersianDate(),
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

		public List<PopularBlogQueryModel> GetPopularBlogsForUI()
		{
			try
			{
				  return Table<Blog>().OrderByDescending(x => x.Likes).Take(6).Select(x =>
					new PopularBlogQueryModel
					{
						Id = x.Id,
						Title = x.Title,
						Author = x.Author,
						ImageName = x.ImageName,
						ImageAlt = x.ImageAlt,
						CreateDate = x.CreateDate.ConvertToPersianDate(),
						Likes = x.Likes,
						Slug = x.Slug
					}).ToList();

				
			}
			catch (Exception e)
			{
				return new();
			}
		}

		public List<LastBlogQueryModel> GetLastBlogsForUI()
		{
			try
			{
				return Table<Blog>().OrderByDescending(x => x.Likes).Take(6).Select(x =>
					new LastBlogQueryModel()
					{
						Id = x.Id,
						Title = x.Title,
						Slug = x.Slug
					}).ToList();


			}
			catch (Exception e)
			{
				return new();
			}
		}

		public List<SpecialBlogForUIQueryModel> GetSpecialBlogsForUI()
		{
			try
			{
				return Table<Blog>().Where(x=> x.IsSpecial).Take(4).Select(x =>
					new SpecialBlogForUIQueryModel()
					{
						Id = x.Id,
						Title = x.Title,
						ImageName = x.ImageName,
						Author = x.Author,
						ImageAlt = x.ImageAlt,
						Slug = x.Slug,
						Likes= x.Likes,
						Category = Table<BlogCategory>().Select(c=> new BlogCategoryQueryModel
						{
							Id = c.Id,
							CreateDate = c.CreateDate,
							Title = c.Title,
							ParentId = c.ParentId,
							Slug = c.Slug,
							IsActive = c.Active,
						}).First(c=> c.Id == x.CategoryId),

						SubCategory = x.SubCategoryId == 0 ? null : Table<BlogCategory>().Select(c => new BlogCategoryQueryModel
						{
							Id = c.Id,
							CreateDate = c.CreateDate,
							Title = c.Title,
							ParentId = c.ParentId,
							Slug = c.Slug,
							IsActive = c.Active,
						}).First(c => c.Id == x.SubCategoryId),
					}).ToList();


			}
			catch (Exception e)
			{
				return new();
			}
		}
	}
}
