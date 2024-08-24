using Microsoft.AspNetCore.Mvc;
using Users.Application.Services.Interfaces;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Roles
{
	[Area("Admin")]
	public class RolesController : Controller
	{
		
		private readonly IRoleService _roleService;

		public RolesController(IRoleService roleService)
		{
			_roleService = roleService;
		}

		public IActionResult Index()
		{
			var model = _roleService.GetRoles();

			return View(model);
		}
	}
}
