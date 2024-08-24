
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users1.Domain.UserAgg;

namespace Users1.Infrastructure.EFConfigs
{
	internal class UserConfig : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> builder)
		{

			builder.HasKey(x => x.Id);

			builder.Property(x=> x.UserName).IsRequired().HasMaxLength(100);
			builder.Property(x=> x.FullName).IsRequired().HasMaxLength(100);
			builder.Property(x=> x.PassKey).IsRequired().HasMaxLength(10);
			builder.Property(b => b.Avatar).IsRequired().HasMaxLength(200);
			builder.Property(x=> x.Email).IsRequired().HasMaxLength(200);
			builder.Property(b => b.Gender).IsRequired();

			// +989112355555 or 09112355555  :  11 and 13 length
			builder.Property(x=> x.Mobile).IsRequired().HasMaxLength(13);
			builder.Property(x=> x.Biography).IsRequired(false).HasMaxLength(1000);

			builder.HasMany(b => b.UserRoles)
				.WithOne(a => a.User).HasForeignKey(a => a.UserId);


		}
	}

	internal class RoleConfig : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{


			builder.HasKey(x => x.Id);

			builder.Property(x => x.Title).IsRequired().HasMaxLength(200);

			builder.HasMany(x => x.Permissions).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);

			builder.HasMany(x => x.UserRoles).WithOne(x => x.Role).HasForeignKey(x => x.RoleId);
		}
	}

	internal class PermissionConfig : IEntityTypeConfiguration<Permission>
	{
		public void Configure(EntityTypeBuilder<Permission> builder)
		{


			builder.HasKey(x => x.Id);
			builder.Property(x => x.UserPermission).IsRequired();

			builder.HasOne(x => x.Role).WithMany(x => x.Permissions).HasForeignKey(x => x.RoleId);
		}
	}
}
