using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Users1.Application.Contract.RoleService.Query;
using Users1.Domain.UserAgg;
using Users1.Infrastructure.Context;

namespace Users1.Query.Services
{
	internal class RoleQuery : BaseRepository , IRoleQuery
	{

		private readonly UsersContext _usersContext;

		public RoleQuery(UsersContext usersContext) : base(usersContext)
		{
			_usersContext = usersContext;
		}

		public async Task<List<RoleQueryModel>?> GetRoles()
		{
			try
			{
				return await Table<Role>().OrderByDescending(x => x.CreateDate).Include(x=> x.Permissions).Select(x => new RoleQueryModel
				{
					Id = x.Id,
					Title = x.Title,
					Permissions = x.Permissions.Select(x=> new PermissionQueryModel
					{
						Id = x.Id,
						UserPermission = x.UserPermission,
						CreateDate = x.CreateDate
					}).ToList(),
					Users = new(),
					CreateDate = x.CreateDate,
					UpdateDate = x.UpdateDate
				}).ToListAsync();
			}
			catch (Exception r)
			{
				return null;
			}
		}
	}
}
