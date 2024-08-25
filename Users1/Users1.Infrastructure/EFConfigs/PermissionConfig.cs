using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users1.Domain.UserAgg;

namespace Users1.Infrastructure.EFConfigs;

internal class PermissionConfig : IEntityTypeConfiguration<Permission>
{
	public void Configure(EntityTypeBuilder<Permission> builder)
	{


		builder.HasKey(x => x.Id);
		builder.Property(x => x.UserPermission).IsRequired();

		builder.HasOne(x => x.Role).WithMany(x => x.Permissions).HasForeignKey(x => x.RoleId);
	}
}