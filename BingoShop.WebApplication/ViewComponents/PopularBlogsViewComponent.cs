﻿using Blogs1.Application.Contract.BlogService.Query;
using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.ViewComponents
{
	public class PopularBlogsViewComponent : ViewComponent
	{

		private readonly IBlogQuery _blogQuery;

		public PopularBlogsViewComponent(IBlogQuery blogQuery)
		{
			_blogQuery = blogQuery;
		}

		public IViewComponentResult Invoke()
		{

			return View(_blogQuery.GetPopularBlogsForUI());

		}
	}
}
