using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Domain.Agg.ArticleAgg;
using Blogs.Domain.Agg.CategoryAgg;
using Blogs.Infrastructure.Context_Config;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Infrastructure.Context
{
	public class Blog_Context : DbContext
	{

		public Blog_Context(DbContextOptions<Blog_Context> options) : base(options)
		{
			
		}

		public DbSet<BlogCategory> BlogCategories { get; set; }

		public DbSet<Article> Articles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new BlogCategoryConfig());
			modelBuilder.ApplyConfiguration(new ArticleConfig());
			base.OnModelCreating(modelBuilder);
		}
	}
}
