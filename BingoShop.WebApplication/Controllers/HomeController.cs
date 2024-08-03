using BingoShop.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Shared.Application.Models;
using Shared.Application.Services;
using Shared.Domain.Enums;
using Users.Application.RequiredPermissions;
namespace BingoShop.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		IAuthService _authService;
		public HomeController(ILogger<HomeController> logger, IAuthService authService)
		{
			_logger = logger;
			_authService = authService;
		}

		public IActionResult Index()
		{


			if (HttpContext.User.Claims.Any())
			{
				var userId = _authService.GetUserId();
				
				return View(userId);
			}
			


			return View();
		}

		[RequiredPermission(UserPermission.AdminPanel)]
		public IActionResult Privacy()
		{
			return View();
		}

		[Route("/not-permitted")]
		public IActionResult NotPermitted()
		{
			return View("_NotPermitted");
		}

		[Route("/Login")]
		public IActionResult SignIn()
		{
			var ok = _authService.Login(new AuthModel(1, "dsfdsfsdfsdfds", "0914545685"));

			if (!ok)
				return View("Error");

			return View("_SignIn");
		}

		public IActionResult Logout()
		{
			var ok = _authService.Logout();
			if (ok)
			{
				return Redirect("./");
			}
			
			return View("Error");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
