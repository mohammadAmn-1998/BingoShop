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

		
		public async Task<List<PopularBlogQueryModel>> GetPopularBlogsForUI()
		{
			try
			{
				  return await Table<Blog>().OrderByDescending(x => x.Likes).Take(6).Select(x =>
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
					}).ToListAsync();

				
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

		public LastBlogTitleQueryModel GetBlogLastTitles()
		{
			try
			{
				List<BlogCategoryQueryModel> categories = new()
				{
					new()
					{
						Id = 0,
						Title = "همه",
					}
				};

				categories.AddRange(Table<BlogCategory>().Where(x => x.ParentId == 0 && x.Active).Take(4).Select(c =>
					new BlogCategoryQueryModel()
					{
						Title = c.Title,
						Id = c.Id,
						Slug = c.Slug

					}).ToList());

				List<LastBlogQueryModel> blogs = new();

				foreach (var category in categories)
				{

					if (category.Id == 0)
					{
						blogs.AddRange(Table<Blog>().Where(x => x.Active).OrderByDescending(x => x.CreateDate).Take(4).Select(b => new LastBlogQueryModel
						{
							Id = b.Id,
							Title = b.Title,
							Slug = b.Slug,
							CategoryTitle = category.Title,
							ImageName = b.ImageName,
							ImageAlt = b.ImageAlt,
							CreateDate = b.CreateDate.ConvertToPersianDate(),
							Author = b.Author,
							Summary = b.Summary
						}).ToList());
					}

					blogs.AddRange(Table<Blog>().Where(x=> x.CategoryId == category.Id && x.Active).OrderByDescending(x=> x.CreateDate).Take(4).Select(b=> new LastBlogQueryModel
					{
						Id = b.Id,
						Title = b.Title,
						Slug = b.Slug,
						CategoryTitle = category.Title,
						ImageName= b.ImageName,
						ImageAlt = b.ImageAlt,
						CreateDate = b.CreateDate.ConvertToPersianDate(),
						Author = b.Author,
						Summary = b.Summary
					}).ToList());

				}

				return new()
				{
					Categories = categories,
					Blogs = blogs
				};

			}
			catch
			{
				return new();
			}
		}
	}
}
