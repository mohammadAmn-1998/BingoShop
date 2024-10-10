using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogService.Query;
using Blogs1.Domain.BlogAgg;
using Blogs1.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Ui.Blog;
using Seos.Domain;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Query.Services.Services
{
	internal class BlogUiQuery : BaseRepository,IBlogUiQuery
	{
		private readonly ISeoRepository _seoRepository;

		public BlogUiQuery(BlogContext context, ISeoRepository seoRepository) : base(context)
		{
			_seoRepository = seoRepository;
		}

		public async Task<BlogUIPaging> GetBlogsForUI(FilterParams filterParams, string categorySlug = "")
		{
			try
			{
				BlogUIPaging model = new();
				model.BreadCrumbs = new();
				model.Seo = new();
				string title = "";
				var blogs = Table<Blog>().Where(x => x.Active);
				if (!string.IsNullOrEmpty(categorySlug))
				{
					
					
					var category = Table<BlogCategory>().FirstOrDefault(x => x.Slug == categorySlug);

					

					if (category != null)
					{
						title =category.Title;

						if (category.ParentId > 0)
						{
							blogs = blogs.Where(x => x.SubCategoryId == category.Id);
							model.BreadCrumbs.Add(new()
							{
								Number = 2,Title = category.Title,Url = ""
							});
							var parent =await GetById<BlogCategory>(category.ParentId);
							if (parent != null)
							{
								model.BreadCrumbs.Add(new()
								{
									Number = 1,
									Title = parent.Title,
									Url = $"/Blog/Blogs?slug={parent.Slug}"
								});
							}
						}
						else
						{
							blogs = blogs.Where(x => x.CategoryId == category.Id);

							model.BreadCrumbs.Add(new()
							{
								Number = 2,
								Title = category.Title,
								Url = ""
							});
						}


						var seo = _seoRepository.GetSeo(category.Id, WhereSeo.BlogCategory);
						if (seo != null)
						{
							model.Seo = new(seo.Where, seo.OwnerId, seo.MetaTitle, seo.MetaDescription,
								seo.MetaKeyWords, seo.IndexPage, seo.Canonical, seo.Schema);

						}


					}
				}

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					title = "نتایج جستجو برای :" + " " + filterParams.Title;

					blogs = blogs.Where(x =>
						x.Title.ToLower().Contains(filterParams.Title.Trim().ToLower()) ||
						x.Summary.Trim().ToLower().Contains(filterParams.Title.Trim().ToLower()) ||
						x.Author.ToLower().Contains(filterParams.Title.Trim().ToLower())).OrderByDescending(x=> x.Id);
				}

				title = title == "" ? "نتایج جستجو برای  : همه" : title;
				model.PageTitle = title;
				model.Slug = categorySlug;
				model.FilterParams = filterParams;
				model.Blogs = new();
				model.GetBasePagination(blogs,filterParams.PageId,4);
				if (blogs.Count() > 0)
				{
					model.Blogs = await blogs.Skip(model.Skip).Take(model.Take).Select(x => new BlogUIQueryModel
					{
						Title = x.Title,
						Summary = x.Summary,
						ImageName = x.ImageName,
						ImageAlt = x.ImageAlt,
						Slug = x.Slug,
						Author = x.Author,
						Category = new(),
						CategoryId = x.SubCategoryId != 0 ? x.SubCategoryId : x.CategoryId,
						CreateDate = x.CreateDate.ConvertToPersianDate(),

					}).ToListAsync();

					model.Blogs.ForEach(x =>
					{
						var category = Table<BlogCategory>().First(c => c.Id == x.CategoryId || c.ParentId == x.CategoryId );
						x.Category.Slug = category.Slug;
						x.Category.Title = category.Title;
					});
				}
			

				return model;


			}
			catch (Exception e)
			{
				return new()
				{
					EntityCount = 0,
					Take = 0,
					TotalPages = 0,
					StartPage = 0,
					CurrentPage = 0,
					EndPage = 0,
					Skip = 0,
					FilterParams = filterParams,
					Blogs = new(),
					Seo = new(),
					BreadCrumbs = new(),
					Slug = "",
					PageTitle = "",

				};
			}
		}
	}
}
