using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users1.Domain.UserAgg;
using Users1.Infrastructure.EFConfigs;

namespace Users1.Infrastructure.Context
{
	public class UsersContext : DbContext
	{

		public DbSet<Users1.Domain.UserAgg.User> Users { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }

		public UsersContext(DbContextOptions<UsersContext> options) : base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			var assembly = typeof(UserConfig).Assembly;
			modelBuilder.ApplyConfigurationsFromAssembly(assembly);
			base.OnModelCreating(modelBuilder);
		}
	}
}
