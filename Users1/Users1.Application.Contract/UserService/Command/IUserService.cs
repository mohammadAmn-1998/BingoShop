using Shared.Application.Utility;

namespace Users1.Application.Contract.UserService.Command;

public interface IUserService
{
	Task<bool> Register(RegisterUser command);
	OperationResult Login(LoginUser command);
	// OperationResult Create(CreateUser command);
	Task<OperationResult> Edit(EditUserByAdmin command);
	Task<OperationResult> EditByUser(EditUserByUser command, int userId);
	EditUserByUser GetForEditByUser(int userId);
	EditUserByAdmin GetForEditByAdmin(int userId);
	Task<bool> ActivationChange(int id);
	Task<bool> BanChange(int id);
	bool Logout();
}