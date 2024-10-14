using Emails.Application.Contract.MessageUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services;
using Shared.Domain.Enums;

namespace BingoShop.WebApplication.Controllers
{
	public class ContactUsController : ControllerBase
	{
		private IMessageUserApplication _messageUserApplication;
		IAuthService _authService;

		public ContactUsController(IMessageUserApplication messageUserApplication, IAuthService authService)
		{
			_messageUserApplication = messageUserApplication;
			_authService = authService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(CreateMessageUser model)
		{
			model.UserId = _authService.GetUserId();
			if (!ModelState.IsValid)
				return View(model);

			var res = _messageUserApplication.Create(model);
			if (res.Status == Status.Success)
			{
				SuccessAlert("ما پیام شما را دریافت کردیم و به زودی به آن پاسخ می دهیم.");
				return Redirect("/");
			}

			ErrorAlert(res.Message);
			return View(model);


		}
	}
}
