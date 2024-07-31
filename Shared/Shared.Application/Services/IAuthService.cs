using Shared.Application.Models;

namespace Shared.Application.Services;

public interface IAuthService
{
	
	bool Login(AuthModel model);
	string GetUserUniqueKey();
	string GetUserMobile();
	int GetUserId();
	bool Logout();

}