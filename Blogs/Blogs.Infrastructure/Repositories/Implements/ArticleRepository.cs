using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Domain.Agg.ArticleAgg;
using Blogs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.BaseRepository;

namespace Blogs.Infrastructure.Repositories.Implements
{
	internal class ArticleRepository : Repository<int,Article>, IArticleRepository
	{
		public ArticleRepository(Blog_Context context) : base(context)
		{
		}
	}
}
