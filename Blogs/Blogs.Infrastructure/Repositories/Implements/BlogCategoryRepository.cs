using Blogs.Domain.Agg.CategoryAgg;
using Blogs.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;
using Shared.Infrastructure.BaseRepository;

namespace Blogs.Infrastructure.Repositories.Implements;

public class BlogCategoryRepository : Repository<int,BlogCategory>, IBlogCategoryRepository
{
	public BlogCategoryRepository(Blog_Context context) : base(context)
	{

	}


}