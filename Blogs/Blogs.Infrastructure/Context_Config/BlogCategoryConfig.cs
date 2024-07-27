using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs.Domain.Agg.CategoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogs.Infrastructure.Context_Config
{
	public class BlogCategoryConfig : IEntityTypeConfiguration<BlogCategory>
	{
		

		public void Configure(EntityTypeBuilder<BlogCategory> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Title).IsRequired(true).HasMaxLength(1000);
			builder.Property(x => x.ImageName).IsRequired(true).HasMaxLength(100);
			builder.Property(x => x.ImageAlt).IsRequired(true).HasMaxLength(500);
			builder.Property(x => x.Slug).IsRequired(true).HasMaxLength(200);
			
		}
	}
}
