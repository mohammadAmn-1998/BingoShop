using Shared.Application.Models;

namespace Shared.Application.Services;

public interface IAuthService
{
	
	bool Login(AuthModel model);
	string GetUserUniqueKey();
	string GetUserMobile();
	string GetUserFullName();
	long GetUserId();
	bool Logout();
	
	string GetUserAvatar();
}