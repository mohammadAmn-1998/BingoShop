using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Domain.BlogAgg;
using Blogs1.Infrastructure.EFConfigs;
using Microsoft.EntityFrameworkCore;

namespace Blogs1.Infrastructure.Context
{
	public class BlogContext : DbContext
	{

		public DbSet<Blog> Blogs { get; set; }

		public DbSet<BlogCategory> BlogCategories { get; set; }

		public BlogContext(DbContextOptions<BlogContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assembly = typeof(BlogConfig).Assembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
