using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;

namespace Comments.Infrastructure.Repositories
{
	internal class CommentRepository : BaseRepository, ICommentRepository
	{
		public CommentRepository(CommentContext context) : base(context)
		{
		}
	}
}
