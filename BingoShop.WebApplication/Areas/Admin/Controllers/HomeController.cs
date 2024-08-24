using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Users.Application.RequiredPermissions;
using Users.Application.Services.Interfaces;

namespace BingoShop.WebApplication.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			
			return View();
		}
	}
}
