using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Domain.Agg.ArticleAgg;
using Blogs.Domain.Agg.CategoryAgg;
using Blogs.Infrastructure.Context;
using Blogs.Infrastructure.Repositories.Implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs.Infrastructure.Bootstrapper
{
	public static class InfraBootstrapper
	{

		public static void InstallBlogServices(IServiceCollection service,string connectionString)
		{

			service.AddTransient<IBlogCategoryRepository, BlogCategoryRepository>();
			service.AddTransient<IArticleRepository, ArticleRepository>();

			service.AddDbContext<Blog_Context>(options =>
			{
				options.UseSqlServer(connectionString);
			});


		}

	}
}
