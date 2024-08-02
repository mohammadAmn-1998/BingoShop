using Microsoft.EntityFrameworkCore;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Infrastructure.BaseRepository;
using Users.Domain.UserAgg;
using Users.Infrastructure.Context;

namespace Users.Infrastructure.Repositories;

internal class RoleRepository : Repository<int, Role> , IRoleRepository
{
	BlogUsers_Context _context;

	public RoleRepository(BlogUsers_Context context) : base(context)
	{
		_context = context;
	}

	public OperationResult Create(Role role, List<UserPermission> permissions)
	{
		try
		{
			if (IsExists(x => x.Title == role.Title.Trim()))
				return new(Status.BadRequest, ErrorMessages.DuplicateError, "Title");

			Insert(role);

			if (permissions.Any())
				foreach (var permission in permissions)
				{

					_context.Permissions.Add(new Permission(role.Id, permission));
				}

			if (Save() > 0) 
			  return new(Status.Success);

			throw new Exception();
		}
		catch (Exception e)
		{

			return new OperationResult(Status.InternalServerError, ErrorMessages.InternalServerError);

		}
		
		


	}

	public OperationResult Delete(int roleId)
	{
		try
		{

			var role = _context.Roles.Single(x => x.Id == roleId);

			Delete(role);

			foreach (var permission in _context.Permissions.Where(x=> x.RoleId == role.Id))
			{

				_context.Permissions.Remove(permission);

			}

			if (Save() > 0)
				return new(Status.Success);

			throw new Exception();
		}
		catch (Exception e)
		{
			return new(Status.InternalServerError, ErrorMessages.InternalServerError);
		}
	}

	public OperationResult Edit(Role role, List<UserPermission> permissions)
	{
		try
		{

			if (IsExists(x => x.Title == role.Title.Trim() && x.Id != role.Id))
				return new(Status.BadRequest, ErrorMessages.DuplicateError);

			Insert(role);

			if (permissions.Count > 0)
			{

				foreach (var permission in role.Permissions)
				{

					_context.Remove(permission);

				}

				if (Save() > 0)
				{
					foreach (var permission in permissions)
					{

						_context.Permissions.Add(new Permission(role.Id, permission));

					}
				}

				if (Save() > 0)
					return new(Status.Success);
			}
				

			throw new Exception();
		}
		catch (Exception e)
		{

			return new(Status.InternalServerError, ErrorMessages.InternalServerError);
		}

	}
}