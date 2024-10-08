using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Query;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Contract.UserService.Query;

using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;


namespace BingoShop.WebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_ادمین)]
	public class HomeController : ControllerBase
	{

		
		public HomeController(IUserQuery userQuery, IUserService userService, IRoleQuery roleQuery)
		{
			
		}

		public IActionResult Index()
		{
			return View();
		}

		

		

		


		


	}
}
