using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Blogs1.Query.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs1.Query.Bootstrapper
{
	public class Blog1QueryBootstrapper
	{

		public static void Config(IServiceCollection services)
		{

			services.AddTransient<IBlogCategoryQuery, BlogCategoryQuery>();

		}

	}
}
