using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users.Application.Dtos.RoleDtos;
using Users.Application.Services.Interfaces;
using Users.Domain.UserAgg;

namespace Users.Application.Services.Implements
{
	internal class RoleService : IRoleService
	{

		private readonly  IRoleRepository _roleRepository;
		private readonly IUserRoleRepository _userRoleRepository;

		public RoleService(IUserRoleRepository userRoleRepository)
		{
			_userRoleRepository = userRoleRepository;
		}

		public OperationResult CreateRole(CreateRoleDto dto, List<UserPermission> permissions)
		{
			return _roleRepository.Create(new Role(dto.Title), permissions);
		}

		public OperationResult DeleteRole(int roleId)
		{

			return _roleRepository.Delete(roleId);
		}

		public OperationResult EditRole(EditRoleDto dto, List<UserPermission> permissions)
		{
			var role = _roleRepository.GetBy(x=> x.Id == dto.RoleId);

			if (role == null)
				return new(Status.NotFound,ErrorMessages.RoleNotFound);

			return _roleRepository.Edit(role, permissions);
		}

		public bool CheckPermission(int userId, UserPermission permission)
		{
			try
			{

				var userRoles = _userRoleRepository.GetUserRoles(userId);
				if(userRoles == null)
					return false;

				foreach (var userRole in userRoles)
				{
					if(userRole.Role.Permissions.Any(x=> x.UserPermission == permission))
						return true;

				}

				return false;
			}
			catch (Exception e)
			{

				return false;
			}
		}
	}
}
