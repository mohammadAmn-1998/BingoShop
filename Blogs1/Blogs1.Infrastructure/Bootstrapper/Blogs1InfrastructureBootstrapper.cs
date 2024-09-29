using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Domain.BlogAgg.IRepositories;
using Blogs1.Infrastructure.Context;
using Blogs1.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blogs1.Infrastructure.Bootstrapper
{
	public static class Blogs1InfrastructureBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{


			services.AddDbContext<BlogContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			services.AddTransient<IBlogRepository, BlogRepository>();
			services.AddTransient<IBlogCategoryRepository, BlogCategoryRepository>();

		}

	}
}
