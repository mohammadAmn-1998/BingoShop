using Blogs.Domain.Agg.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blogs.Infrastructure.Context_Config;

public class ArticleConfig : IEntityTypeConfiguration<Article>
{

	public void Configure(EntityTypeBuilder<Article> builder)
	{
		builder.HasKey(x => x.Id);
		builder.Property(x => x.Title).IsRequired(true).HasMaxLength(1000);
		builder.Property(x => x.ImageName).IsRequired(true).HasMaxLength(100);
		builder.Property(x => x.ImageName).IsRequired(true).HasMaxLength(100);
		builder.Property(x => x.Slug).IsRequired(true).HasMaxLength(200);
		builder.Property(x => x.Author).IsRequired(true).HasMaxLength(200);
		builder.Property(x => x.Summary).IsRequired(true).HasMaxLength(1000);
		builder.Property(x => x.Summary).IsRequired(true);

	}
}