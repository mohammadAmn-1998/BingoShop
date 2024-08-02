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

		
	}
}
