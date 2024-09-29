using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogService.Command;
using Blogs1.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blogs1.Application.Bootstrapper
{
	public static class Blogs1ApplicationBootstrapper
	{


		public static void Config(IServiceCollection services)
		{

			services.AddTransient<IBlogCategoryService, BlogCategoryService>();
			services.AddTransient<IBlogService, BlogService>();

		}
	}
}
