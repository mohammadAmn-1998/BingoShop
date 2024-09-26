using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.Context;
using Comments.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Infrastructure.Bootstrapper
{
	public static class CommentsInfrastructureBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			services.AddDbContext<CommentContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			services.AddTransient<ICommentRepository, CommentRepository>();

		}

	}
}
