using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Application.Contract.CommentService.Command;
using Comments.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Comments.Application.Bootstrapper
{
	public static class CommentsApplicationBootstrapper
	{

		public static void Config(IServiceCollection services)
		{

			services.AddTransient<ICommentService,CommentService>();

		}

	}
}
