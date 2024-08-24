using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Users1.Domain.UserAgg
{
	public class Role : BaseEntity<long>
	{

		public string Title { get; private set; }

		public List<Permission> Permissions { get; private set; }
		public List<UserRole> UserRoles { get; private set; }

		public Role()
		{
			Permissions = new();
			UserRoles = new();
		}

		public Role(string title)
		{
			Title = title;
		}
		public void Edit(string title)
		{
			Title = title;
		}
	}
}
