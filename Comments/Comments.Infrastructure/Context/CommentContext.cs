using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comments.Domain.CommentAgg;
using Comments.Infrastructure.EFConfigs;
using Microsoft.EntityFrameworkCore;

namespace Comments.Infrastructure.Context
{
	public class CommentContext : DbContext
	{

		public CommentContext(DbContextOptions<CommentContext> options) : base(options)
		{
			
		}

		public DbSet<Comment> Comments { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assembly = typeof(CommentConfig).Assembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assembly);

			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;

			}
		}
	}
}
