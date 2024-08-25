namespace Users1.Application.Contract.UserService.Query;

public interface IUserQuery
{
	UserQueryModel? GetUserBy(long userId);
	UserQueryModel? GetUserByMobile(string mobile);
}