using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Comments.Infrastructure.Context
{
	public class CommentContext : DbContext
	{

		public CommentContext(DbContextOptions<CommentContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			var assembly = typeof(CommentContext).Assembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assembly);

			base.OnModelCreating(modelBuilder);

			foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.Restrict;

			}
		}
	}
}
