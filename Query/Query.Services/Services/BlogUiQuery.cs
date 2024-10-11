using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogService.Query;
using Blogs1.Domain.BlogAgg;
using Blogs1.Infrastructure.Context;
using Comments.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Query.Contract.Ui.Blog;
using Query.Contract.Ui.Seo;
using Seos.Domain;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Users1.Domain.UserAgg.IRepositories;

namespace Query.Services.Services
{
	internal class BlogUiQuery : BaseRepository,IBlogUiQuery
	{
		private ISeoUIQuery _seoUiQuery;
		private IUserRepository _userRepository;
		private readonly ICommentRepository _commentRepository;

		public BlogUiQuery(BlogContext context,ISeoUIQuery seoUiQuery, IUserRepository userRepository, ICommentRepository commentRepository) : base(context)
		{
			_seoUiQuery = seoUiQuery;
			_userRepository = userRepository;
			_commentRepository = commentRepository;
		}

		public async Task<BlogUIPaging> GetBlogsForUI(FilterParams filterParams, string categorySlug = "")
		{
			try
			{
				BlogUIPaging model = new();
				model.BreadCrumbs = GetArchiveBreadCrumb();
				string title = "";
				model.Seo = new();
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
							var parent =await GetById<BlogCategory>(category.ParentId);
							if (parent != null)
							{
								model.BreadCrumbs = GetArchiveBreadCrumb(parent.Slug, category.Slug);
							}
						}
						else
						{
							blogs = blogs.Where(x => x.CategoryId == category.Id);

							model.BreadCrumbs = GetArchiveBreadCrumb(category.Slug);
						}

						model.Seo =await _seoUiQuery.GetSeoForUI(category.Id, WhereSeo.BlogCategory, category.Title);



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
					model.Blogs = await blogs.Skip(model.Skip).Take(model.Take).Select(x => new BlogUiQueryModel
					{
						Title = x.Title,
						Summary = x.Summary,
						ImageName = x.ImageName,
						ImageAlt = x.ImageAlt,
						Slug = x.Slug,
						Author = x.Author,
						Category = new(),
						CategoryId = x.SubCategoryId != 0 ? x.SubCategoryId : x.CategoryId,
						CreateDate = x.CreateDate.ConvertToPersianDate()

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

		public async Task<SingleBlogUIQueryModel> GetSingleBlog(string slug)
		{
			try
			{

				List<BreadCrumbUiQueryModel> breadCrumbs = new();
				
				CategoryUIQueryModel categoryUi = new();
				CategoryUIQueryModel subCategoryUi = new();

				var blog = Table<Blog>().First(x => x.Slug == slug);

				var category = await GetById<BlogCategory>(blog.CategoryId);
				
					categoryUi.Slug = category.Slug;
					categoryUi.Title = category.Title;
				

				if (blog.SubCategoryId > 0)
				{
					var sub =await GetById<BlogCategory>(blog.SubCategoryId);
					
						subCategoryUi.Slug = sub.Slug;
						subCategoryUi.Title = sub.Title;
					

					breadCrumbs = GetArchiveBreadCrumb(category!.Slug, sub!.Slug);
				}

				var seo = await _seoUiQuery.GetSeoForUI(blog.Id, WhereSeo.Blog, blog.Title);
				var author = _userRepository.GetById(blog.UserId);


				return new()
				{
					Id = blog.Id,
					Title = blog.Title,
					Summary = blog.Summary,
					Content = blog.Content,
					CreateDate = blog.CreateDate.ConvertToPersianDate(),
					ImageName = blog.ImageName,
					ImageAlt = blog.ImageAlt,
					Likes = blog.Likes,
					CategoryId = blog.CategoryId,
					SubCategoryId = blog.SubCategoryId,
					Author = author!.FullName,
					AuthorAvatar = author.Avatar,
					AuthorAvatarImageAlt = author.FullName,
					TotalComments = _commentRepository.GetCommentsCountForUi(blog.Id, CommentFor.بلاگ),
					TotalVisits = blog.TotalVisits,
					Category = categoryUi,
					SubCategory = subCategoryUi,
					Seo = seo,
					BreadCrumbs = breadCrumbs
				};





			}
			catch (Exception e)
			{
				return new()
				{
					Title = "در مورد این مقاله مشکلی وجود دارد!"
				};
			}
		}


		private List<BreadCrumbUiQueryModel> GetArchiveBreadCrumb(string? categorySlug =null , string? subCategorySlug = null)
		{
			List<BreadCrumbUiQueryModel> model = new()
			{
				new BreadCrumbUiQueryModel(){Number = 1 , Title ="خانه" , Url = "/"} ,
				new BreadCrumbUiQueryModel(){Number = 2, Title = "مجله خبری" , Url = "/Blog"},
				new BreadCrumbUiQueryModel(){Number = 3, Title = "آرشیو مقالات" , Url = "/Blog/Blogs"}
			};

			if (string.IsNullOrEmpty(categorySlug) && string.IsNullOrEmpty(subCategorySlug))
				model.Add(new()
					{ Number = 4, Title = "نتایج جستجو", Url = "" }
				);

			if (!string.IsNullOrEmpty(categorySlug))
			{
				var category = Table<BlogCategory>().FirstOrDefault(x=> x.Slug == categorySlug);
				if (category != null)
					model.Add(new BreadCrumbUiQueryModel() { Number = 4, Title = category.Title, Url = subCategorySlug !=null ? $"/blog/blogs?slug={category.Slug}" : "" });
			}
			if (!string.IsNullOrEmpty(subCategorySlug))
			{
				var category = Table<BlogCategory>().FirstOrDefault(x => x.Slug == subCategorySlug );
				if (category != null)
					model.Add(new BreadCrumbUiQueryModel() { Number = 5, Title = category.Title, Url = "" });
			}
			
			
			return model;
		}
	}
}
