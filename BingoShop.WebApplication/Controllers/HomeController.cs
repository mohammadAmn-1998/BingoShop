using BingoShop.WebApplication.Models;
using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Application.Contract.MessageUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services;
using Shared.Domain.Enums;

namespace BingoShop.WebApplication.Controllers
{
	public class HomeController : ControllerBase
	{
		private readonly ILogger<HomeController> _logger;
		private readonly IEmailUserApplication _emailUserApplication;
		private readonly IAuthService _authService;
		private readonly IMessageUserApplication _messageUserApplication;
		public HomeController(ILogger<HomeController> logger, IEmailUserApplication emailUserApplication, IAuthService authService, IMessageUserApplication messageUserApplication)
		{
			_logger = logger;
			_emailUserApplication = emailUserApplication;
			_authService = authService;
			_messageUserApplication = messageUserApplication;
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

		[HttpPost]
		[Route("/Home/AddUserEmail")]
		public JsonResult AddUserEmail(string email)
		{
			if (string.IsNullOrEmpty(email))
				return Json(new { ok = false, message = "مشکلی در عملیات وجود دارد. دوباره تلاش کنید!" });

			var result = _emailUserApplication.Create(new CreateEmailUser()
			{
				Email = email.Trim(),
				UserId = _authService.GetUserId()
			});


			if (result.Status == Status.Success)
				return Json(new { ok = true });

			return Json(new{ok = false,message = result.Message});
		}

	}
}
