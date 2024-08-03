using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Domain.UserAgg;
using Users.Infrastructure.Config;

namespace Users.Infrastructure.Context
{
	internal class BlogUsers_Context : DbContext
	{

		public DbSet<User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<UserAddress> UserAddresses { get; set; }


		public BlogUsers_Context(DbContextOptions<BlogUsers_Context> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.ApplyConfiguration(new UserConfig());
			modelBuilder.ApplyConfiguration(new PermissionConfig());
			modelBuilder.ApplyConfiguration(new UserAddressConfig());
			modelBuilder.ApplyConfiguration(new RoleConfig());
			modelBuilder.ApplyConfiguration(new UserRoleConfig());

			base.OnModelCreating(modelBuilder);
		}

		
	}
}
