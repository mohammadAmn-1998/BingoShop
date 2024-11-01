﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Domain.BlogAgg.IRepositories;
using Query.Contract.Admin.Seo;
using Seos.Domain;
using Shared.Domain.Enums;
using Site.Domain.SitePageAgg;

namespace Query.Services.Services
{
	internal class SeoAdminQuery : ISeoAdminQuery
	{

		private readonly IBlogRepository _blogRepository;
		private readonly IBlogCategoryRepository _blogCategoryRepository;
		private readonly ISitePageRepository _sitePageRepository;


		public SeoAdminQuery(IBlogRepository blogRepository, IBlogCategoryRepository blogCategoryRepository,
			ISitePageRepository sitePageRepository)
		{
			_blogRepository = blogRepository;
			_blogCategoryRepository = blogCategoryRepository;
			_sitePageRepository = sitePageRepository;
		}


		public async Task<string> GetAdminSeoTitle(WhereSeo where, long ownerId)
		{
			switch (where)
			{
				case WhereSeo.Product:
					// GetProductTitle
					return "";
				case WhereSeo.ProductCategory:
					// GetProductCategoryTitle
					return "";
				case WhereSeo.Blog:
					if (ownerId < 1) return "Seo برای صفحه ی اصلی مقالات سایت";
					var blog = await _blogRepository.GetById(ownerId);
					if (blog == null) return "";
					return $"seo برای مقاله {blog.Title}";
				case WhereSeo.BlogCategory:
					if (ownerId < 1) return "seo برای صفحه اصلی مقالات";
					var blogCategory = await _blogCategoryRepository.GetById(ownerId);
					if (blogCategory == null) return "";
					return $"seo برای دسته بندی مقاله {blogCategory.Title}";
				case WhereSeo.Home:
					return "seo برای صفحه اصلی سایت";
				case WhereSeo.AboutUs:
					return "seo برای صفحه درباره ما";
				case WhereSeo.Contact:
					return "seo برای صفحه تماس با ما";
				case WhereSeo.Page:
					if (ownerId < 1) return "";
					var page = await _sitePageRepository.GetById(ownerId);
					if (page == null) return "";
					return $"seo برای صفحه {page.Title}";
				case WhereSeo.PostPackage:
					return "seo برای صفحه پکیج های فروش Api پست";
				default:
					return "";
			}
		}
	}
}
