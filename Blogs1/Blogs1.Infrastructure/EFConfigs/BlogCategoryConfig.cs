using Blogs1.Domain.BlogAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogs1.Infrastructure.EFConfigs
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
