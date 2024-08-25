using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;
using Users1.Application.Contract.RoleService.Command;
using Users1.Domain.UserAgg;
using Users1.Domain.UserAgg.IRepositories;
using Users1.Infrastructure.Context;

namespace Users1.Infrastructure.Repositories;

internal class RoleRepository : BaseRepository, IRoleRepository
{
	public RoleRepository(UsersContext context) : base(context)
	{
	}


	public bool ExistBy(string title)
	{
		try
		{
			return Table<Role>().Any(x => x.Title == title.Trim());
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public Role? GetById(long id)
	{
		try
		{
			return Table<Role>().Include(x => x.Permissions).SingleOrDefault(x => x.Id == id);
		}
		catch (Exception e)
		{
			return null;
		}
	}

	public async Task<bool> Create(CreateRole command)
	{
		try
		{

			var role = new Role(command.Title.Trim());
			Insert(role);
			if (await Save() <= 0)
				throw new Exception();

			if (command.Permissions.Any())
			{

				foreach (var userPermission in command.Permissions)
				{
					var permission = new Permission(role.Id, userPermission);
					Insert(permission);
					
				}
				if (await Save() <= 0)
					throw new Exception();
			}

			return true;

		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> Edit(EditRole command)
	{
		try
		{

			var role = Table<Role>().Include(x => x.Permissions).SingleOrDefault(x => x.Id == command.Id);
			if (role is null) throw new NullReferenceException();

			role.Edit(command.Title);
			if(await Save() <= 0) throw new Exception();

			var permissions = Table<Permission>().Where(x => x.RoleId == command.Id).ToList();
			if (permissions.Any())
			{
				Delete(permissions);
				if (await Save() <= 0) throw new Exception();
			}

			if (command.Permissions.Any())
			{
				
				foreach (var editedPermission in command.Permissions)
				{
					var permission = new Permission(role.Id, editedPermission.UserPermission);
					Update(permission);

				}

				if (await Save() <= 0) throw new Exception();

			}

			return true;

		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> AddRolesToUser(long userId, List<long> roles)
	{
		try
		{

			foreach (var roleId in roles)
			{
				var userRole = new UserRole(userId, roleId);
				Insert(userRole);
			}

			return  await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> DeleteUserRoles(List<UserRole> userRoles)
	{
		try
		{
			Delete(userRoles);
			return await Save() > 0;

		}
		catch (Exception e)
		{
			return false;
		}
	}
}