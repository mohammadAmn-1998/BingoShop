using Shared.Application.Utility;
using Users.Application.Services.Dtos.UserDtos;

namespace Users.Application.Services.Interfaces;

public interface IUserService
{
	OperationResult Create(CreateUserDto dto);
	OperationResult Login(LoginUserDto dto);
	OperationResult EditByUser(EditUserByUserDto dto);
	EditUserByUserDto GetForEditByUser(int userId);
	EditUserByAdminDto GetForEditByAdmin(int userId);
	bool ExistsByUserName(string username);
	bool ExistsByEmail(string email);
	bool ExistsByMobile(string mobile);
	bool ExistsByUserNameAndPassword(string userName,string password);
	void ChangeActivation(int userId);
	void ChangeBlock(int userId);

}