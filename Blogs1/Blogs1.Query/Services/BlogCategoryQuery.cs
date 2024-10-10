using Blogs1.Application.Contract.BlogCategoryService.Query;
using Blogs1.Domain.BlogAgg;
using Blogs1.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;
using Shared.Application.Models;
using Shared.Domain.SeedWorks.Base;
using Exception = System.Exception;

namespace Blogs1.Query.Services
{
	internal class BlogCategoryQuery : BaseRepository , IBlogCategoryQuery 
	{
		public BlogCategoryQuery(BlogContext context) : base(context)
		{
		}


		public async Task<FilteredBlogCategoryQueryModel?> GetFilteredBlogCategories(FilterParams filterParams)
		{
			try
			{

				var result = Table<BlogCategory>().Where(x=> x.ParentId == 0).AsQueryable();

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					result = result.Where(x => x.Title.Contains(filterParams.Title.Trim()));
				}

				var skip = (filterParams.PageId - 1) * filterParams.Take;

				

				FilteredBlogCategoryQueryModel model = new();

				model.FilterParams = filterParams;
				model.GetBasePagination(result,filterParams.PageId,filterParams.Take);
				result = result.Skip(skip).Take(filterParams.Take).OrderByDescending(x => x.CreateDate);

				model.BlogCategories = await result.Select( x => new BlogCategoryQueryModel
				{
					Id = x.Id,
					CreateDate = x.CreateDate,
					Title = x.Title,
					ImageName = x.ImageName,
					ImageAlt = x.ImageAlt,
					ParentId = x.ParentId,
					Slug = x.Slug,
					IsActive = x.Active,
					SubCategories =  Table<BlogCategory>().Where(s=> s.ParentId == x.Id).Select(s => new BlogCategoryQueryModel
				{
					Id = s.Id,
					CreateDate = s.CreateDate,
					Title = s.Title,
					ImageName = s.ImageName,
					ImageAlt = s.ImageAlt,
					ParentId = s.ParentId,
					Slug = s.Slug,
					IsActive = s.Active,
					SubCategories = null
				}).ToList()
				}).ToListAsync();


				return model;
			}
			catch (Exception e)
			{
				return null;
			}
		}

		public async Task<List<BlogCategoryQueryModel>?> GetAllCategories()
		{
			try
			{
				var categories = Table<BlogCategory>().Where(x => x.ParentId == 0);

				return await categories.Select(x => new BlogCategoryQueryModel
				{
					Id = x.Id,
					CreateDate = x.CreateDate,
					Title = x.Title,
					ImageName = x.ImageName,
					ImageAlt = x.ImageAlt,
					ParentId = x.ParentId,
					Slug = x.Slug,
					IsActive = x.Active,
					SubCategories = Table<BlogCategory>().Where(s=> s.ParentId == x.Id).Select(s=> new BlogCategoryQueryModel
					{
						Id = s.Id,
						CreateDate = s.CreateDate,
						Title = s.Title,
						ImageName = s.ImageName,
						ImageAlt = s.ImageAlt,
						ParentId = s.ParentId,
						Slug = s.Slug,
						IsActive = s.Active,
						SubCategories = null
					}).ToList()
				}).ToListAsync();

			}
			catch (Exception e)
			{

				return null;

			}
		}

		public async Task<List<BlogCategoryQueryModel>?> GetSubCategories(long parentId)
		{
			try
			{
				var categories = Table<BlogCategory>().Where(x => x.ParentId == parentId);

				return await categories.Select(x => new BlogCategoryQueryModel
				{
					Id = x.Id,
					CreateDate = x.CreateDate,
					Title = x.Title,
					ImageName = x.ImageName,
					ImageAlt = x.ImageAlt,
					ParentId = x.ParentId,
					Slug = x.Slug,
					IsActive = x.Active,
					SubCategories = Table<BlogCategory>().Where(s => s.ParentId == x.Id).Select(s => new BlogCategoryQueryModel
					{
						Id = s.Id,
						CreateDate = s.CreateDate,
						Title = s.Title,
						ImageName = s.ImageName,
						ImageAlt = s.ImageAlt,
						ParentId = s.ParentId,
						Slug = s.Slug,
						IsActive = s.Active,
						SubCategories = null
					}).ToList()
				}).ToListAsync();

			}
			catch (Exception e)
			{

				return null;

			}
		}

		public async Task<List<SelectListItem>?> GetAllCategoriesAsSelectList()
		{
			try
			{
				List<SelectListItem> categories = new()
				{
					new SelectListItem("لطفا انتخاب کنید",0.ToString())
				};

				var result = await Table<BlogCategory>().Where(x => x.ParentId == 0).ToListAsync();

				if (result.Any())
				{
					foreach (var blogCategory in result)
					{
						categories.Add(new SelectListItem(blogCategory.Title,blogCategory.Id.ToString()));
					}
				}

				return categories;
			}
			catch (Exception )
			{
				return null;
			}
		}

		public async Task<List<SelectListItem>?> GetSubCategoriesAsSelectList(long parentId)
		{
			try
			{
				List<SelectListItem> categories = new()
				{
					new SelectListItem("لطفا انتخاب کنید",0.ToString())
				};

				var result = await Table<BlogCategory>().Where(x => x.ParentId == parentId).ToListAsync();

				if (result.Any())
				{
					foreach (var blogCategory in result)
					{
						categories.Add(new SelectListItem(blogCategory.Title, blogCategory.Id.ToString()));
					}
				}

				return categories;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public async Task<string> GetBlogCategoryTitle(long id)
		{
			try
			{
				var blogCategory= await Table<BlogCategory>().FirstOrDefaultAsync(x=> x.Id == id);

				return blogCategory?.Title ?? "";
			}
			catch
			{
				return "";
			}
		}

		public async Task<List<WidgetCategoryForUIQueryModel>> GetCategoriesForWidgetForUi(int count)
		{

			try
			{
				var categories =await Table<BlogCategory>().Where(x => x.ParentId == 0 && x.Active).Select(x =>
					new WidgetCategoryForUIQueryModel
					{
						Title = x.Title,
						Slug = x.Slug,
						BlogsCount = Table<Blog>().Count(b => b.CategoryId == x.Id)
					}).ToListAsync();

				return categories;
			}
			catch (Exception e)
			{
				return new();
			}

		}
	}
}
