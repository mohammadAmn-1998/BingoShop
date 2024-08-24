using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users.Application.Dtos.RoleDtos;

namespace Users.Application.Services.Interfaces
{
	public interface IRoleService
	{
		List<RoleDto>? GetRoles(); 

		OperationResult CreateRole(CreateRoleDto dto, List<UserPermission> permissions);

		OperationResult DeleteRole(int roleId);

		OperationResult EditRole(EditRoleDto dto, List<UserPermission> permissions);

		bool CheckPermission(int userId, UserPermission permission);

	}
}
