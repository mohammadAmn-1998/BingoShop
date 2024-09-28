using Comments.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Comments.Infrastructure.EFConfigs
{
	internal class CommentConfig : IEntityTypeConfiguration<Comment>
	{
		public void Configure(EntityTypeBuilder<Comment> builder)
		{
			builder.ToTable("Comments");
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Text).IsRequired(true).HasMaxLength(3000);
			builder.Property(x => x.Email).IsRequired(false).HasMaxLength(200);
			builder.Property(x => x.FullName).IsRequired(true).HasMaxLength(100);
			builder.Property(x => x.WhyRejected).IsRequired(false).HasMaxLength(3000);
			builder.HasMany(x => x.ChildComments).WithOne(x => x.ParentComment).HasForeignKey(x => x.ParentId);

			

		}
	}
}
