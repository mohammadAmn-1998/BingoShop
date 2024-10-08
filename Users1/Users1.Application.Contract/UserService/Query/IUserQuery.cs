using Shared.Application.Models;
using Shared.Domain.Enums;

namespace Users1.Application.Contract.UserService.Query;

public interface IUserQuery
{
	List<UserQueryModel>? GetAll();
	UserQueryModel? GetUserBy(long userId);
	UserQueryModel? GetUserByMobile(string mobile);
	FilteredUsersQueryModel GetFilteredUsers(FilterParams  filterParams);
	List<UserPermission> GetUserPermissionsById(long userId);
	List<string> GetUserRolesTitleBy(long userId);
}