using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Users1.Application.Contract.RoleService.Query;
using Users1.Application.Contract.UserService.Query;
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
				return  await Table<Role>().OrderByDescending(x => x.CreateDate).Include(x=> x.Permissions).Include(x=> x.UserRoles).ThenInclude(x=> x.User).Select(x => new RoleQueryModel
				{
					Id = x.Id,
					Title = x.Title,
					Permissions = x.Permissions.Select(x=> new PermissionQueryModel
					{
						Id = x.Id,
						UserPermission = x.UserPermission,
						CreateDate = x.CreateDate
					}).ToList(),
					UserRoles = x.UserRoles.Select(x => new UserRoleQueryModel
					{
						Id = x.Id,
						UserId = x.UserId,
						RoleId = x.RoleId,
						Role = new(),
						User = new UserQueryModel
						{
							userId = x.User.Id,
							UserName = x.User.UserName,
							FullName = x.User.FullName,
							Avatar = x.User.Avatar,
							UserUniqueCode = x.User.UserUniqueCode,
							biography = x.User.Biography,
							Gender = x.User.Gender,
							Mobile = x.User.Mobile,
							Email = x.User.Email,
							Roles = new(),
							IsActive = x.User.Active
						}
					}).ToList(),
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
