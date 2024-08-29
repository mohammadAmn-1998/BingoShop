using Shared.Application.Utility;

namespace Users1.Application.Contract.UserService.Command;

public interface IUserService
{
	Task<bool> Register(RegisterUser command);
	OperationResult Login(LoginUser command);
	// OperationResult Create(CreateUser command);
	Task<OperationResult> Edit(EditUserByAdmin command);
	Task<OperationResult> EditByUser(EditUserByUser command, long userId);
	EditUserByUser GetForEditByUser(long userId);
	EditUserByAdmin GetForEditByAdmin(long userId);
	Task<bool> ActivationChange(long id);
	Task<bool> BanChange(long id);
	Task<bool> MobileExists(string mobile);
	Task<bool> EmailExists(string email);
	bool Logout();
}