using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Application.Services.Implements;
using Blogs.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs.Application.Bootstrapper
{
	public static class BlogServiceBootstrapper
	{

		public static void Config(IServiceCollection service)
		{

			service.AddTransient<IBlogCategoryService, BlogCategoryService>();
			service.AddTransient<IArticleService, ArticleService>();

		}

	}
}
