using BingoShop.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.Controllers
{
	public class HomeController : ControllerBase
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[Route("/not-permitted")]
		public IActionResult NotPermitted()
		{
			return View("_NotPermitted");
		}

		

	}
}
