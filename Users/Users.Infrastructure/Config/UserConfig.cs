using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.UserAgg;

namespace Users.Infrastructure.Config;

internal class UserConfig : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");
		builder.HasKey(b => b.Id);

		builder.Property(b => b.FirstName).IsRequired(false).HasMaxLength(255);
		builder.Property(b => b.LastName).IsRequired(false).HasMaxLength(255);
		builder.Property(b => b.Avatar).IsRequired().HasMaxLength(200);
		builder.Property(b => b.EmailAddress).IsRequired(false).HasMaxLength(255);
		builder.Property(b => b.Biography).IsRequired(false).HasMaxLength(1000);
		builder.Property(b => b.Mobile).IsRequired().HasMaxLength(13);
		builder.Property(b => b.Gender).IsRequired();
		builder.Property(b => b.Password).IsRequired().HasMaxLength(100);

		builder.HasMany(b => b.UserAddresses)
			.WithOne(a => a.User).HasForeignKey(a => a.UserId);

		builder.HasMany(b => b.UserRoles)
			.WithOne(a => a.User).HasForeignKey(a => a.UserId);

			
	}
}