using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Users1.WebUI.Utility;
using ControllerBase = Users1.WebUI.Controllers.ControllerBase;

namespace Users1.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class HomeController : ControllerBase 
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
