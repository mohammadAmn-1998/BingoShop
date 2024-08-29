using Shared.Domain.Enums;
using Users1.Application.Contract.UserService.Command;

namespace Users1.Domain.UserAgg.IRepositories;

public interface IUserRepository
{
	
	Task<bool> CheckPermission(long  userId,UserPermission permission);

	User? GetByMobile (string mobile);

	User? GetById(long id);

	Task<bool> ChangePassKey(string mobile,string passKey);

	Task<bool> ChangeActivation(long userId);
	Task<bool> ChangeBan(long userId);

	Task<bool> Create(User user);

	Task<bool> EditByAdmin(EditUserByAdmin command);
	Task<bool> EditByUser(EditUserByUser command);
	Task<bool> MobileExists(string mobile);
	Task<bool> EmailExists(string email);

}