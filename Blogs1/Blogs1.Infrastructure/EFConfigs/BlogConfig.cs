using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blogs1.Domain.BlogAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogs1.Infrastructure.EFConfigs
{
	internal class BlogConfig : IEntityTypeConfiguration<Blog>
	{
		public void Configure(EntityTypeBuilder<Blog> builder)
		{

			builder.HasKey(x => x.Id);
			builder.Property(x => x.Title).IsRequired(true).HasMaxLength(1000);
			builder.Property(x => x.ImageName).IsRequired(true).HasMaxLength(100);
			builder.Property(x => x.ImageAlt).IsRequired(true).HasMaxLength(100);
			builder.Property(x => x.Slug).IsRequired(true).HasMaxLength(200);
			builder.Property(x => x.Author).IsRequired(true).HasMaxLength(200);
			builder.Property(x => x.Summary).IsRequired(true).HasMaxLength(1000);
			
		}
	}
}
