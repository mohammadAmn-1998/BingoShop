
using Microsoft.AspNetCore.Mvc;

namespace BingoShop.WebApplication.Controllers
{
	public class BlogController : ControllerBase
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
