using Shared.Application.Models;

namespace Users1.Application.Contract.UserService.Query;

public interface IUserQuery
{
	List<UserQueryModel>? GetAll();
	UserQueryModel? GetUserBy(long userId);
	UserQueryModel? GetUserByMobile(string mobile);
	FilteredUsersQueryModel GetFilteredUsers(FilterParams  filterParams);
}