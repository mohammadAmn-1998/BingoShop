using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Users1.WebUI.Utility;

namespace Users1.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
