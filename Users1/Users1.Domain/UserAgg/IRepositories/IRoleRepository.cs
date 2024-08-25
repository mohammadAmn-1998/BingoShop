using Users1.Application.Contract.RoleService.Command;

namespace Users1.Domain.UserAgg.IRepositories;

public interface IRoleRepository
{

	bool ExistBy(string title);
	Role? GetById(long id);
	Task<bool> Create(CreateRole command);
	Task<bool> Edit(EditRole command);
	Task<bool> AddRolesToUser(long userId, List<long> roles);
	Task<bool> DeleteUserRoles(List<UserRole> userRoles);
}