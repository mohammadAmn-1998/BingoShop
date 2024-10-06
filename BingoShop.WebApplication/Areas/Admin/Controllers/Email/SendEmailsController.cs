using BingoShop.WebApplication.Utility;
using Emails.Application.Contract.SendEmailApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.SendEmail;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Email
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AddBlog)]
	public class SendEmailsController : ControllerBase
	{
		private readonly ISendEmailAdminQuery _sendEmailAdminQuery;
		private readonly ISendEmailApplication _sendEmailApplication;
		public SendEmailsController(ISendEmailAdminQuery sendEmailAdminQuery, ISendEmailApplication sendEmailApplication)
		{
			_sendEmailAdminQuery = sendEmailAdminQuery;
			_sendEmailApplication = sendEmailApplication;
		}

		public async Task<IActionResult> Index(int pageId = 1,int take = 10,string q="")
		{

			return View(await _sendEmailAdminQuery.GetSendEmailsForAdmin(new(pageId,take,q)));
		}


		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(CreateSendEmail model)
		{

			if (!ModelState.IsValid)
				return View(model);

			var result = _sendEmailApplication.Create(model);

			if(result.Status ==  Status.Success)
				SuccessAlert("متن پیام ایمیل خبرنامه جدید ثبت شد!");
			else
			{
				ErrorAlert(result.Message);
			}

			return RedirectToAction("Index");
		}

		public  IActionResult Detail(long id)
		{

			return View(_sendEmailAdminQuery.GetSendEmailDetailForAdmin(id));
		}

	}
}
