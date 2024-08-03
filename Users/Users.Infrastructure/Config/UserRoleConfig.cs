using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Users.Domain.UserAgg;

namespace Users.Infrastructure.Config;

internal class UserRoleConfig : IEntityTypeConfiguration<UserRole>
{
	public void Configure(EntityTypeBuilder<UserRole> builder)
	{
		builder.ToTable("UserRoles");
		builder.HasKey(b => b.Id);

		
		builder.HasOne(b => b.Role).WithMany(x=> x.UserRoles).HasForeignKey(a => a.RoleId);
		builder.HasOne(b => b.User).WithMany(x=> x.UserRoles).HasForeignKey(a => a.UserId);
	}
}