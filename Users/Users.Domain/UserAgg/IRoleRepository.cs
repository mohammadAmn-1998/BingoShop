using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Users.Domain.UserAgg;

public interface IRoleRepository : IRepository<int, Role>
{

	OperationResult Create(Role role, List<UserPermission> permissions);

	OperationResult Delete(int roleId);

	OperationResult Edit(Role role, List<UserPermission> permissions);

}