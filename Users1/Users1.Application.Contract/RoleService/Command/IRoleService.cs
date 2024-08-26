using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Users1.Application.Contract.RoleService.Command;

public interface IRoleService
{

	Task<bool> CheckPermission(long userId,UserPermission  permission);

    Task<OperationResult> CreateRole(CreateRole command);

    Task<OperationResult> EditRole(EditRole command);

    EditRole GetRoleForEdit(long roleId);

    Task<OperationResult> EditUserRole(long userId, List<long> roles);

}