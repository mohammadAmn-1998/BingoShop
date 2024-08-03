using Shared.Domain.SeedWorks.Base;

namespace Users.Domain.UserAgg;

public interface IUserRoleRepository : IRepository<int,UserRole>
{


	List<UserRole>? GetUserRoles(int userId);

}