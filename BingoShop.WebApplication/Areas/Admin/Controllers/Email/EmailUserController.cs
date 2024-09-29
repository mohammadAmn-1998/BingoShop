using BingoShop.WebApplication.Utility;
using Emails.Application.Contract.EmailUserApplication.Command;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.EmailUser;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Email
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class EmailUserController : ControllerBase
	{

		private readonly IEmailUserAdminQuery _emailUserAdminQuery;
		private readonly IEmailUserApplication _emailUserApplication;

		public EmailUserController(IEmailUserAdminQuery emailUserAdminQuery, IEmailUserApplication emailUserApplication)
		{
			_emailUserAdminQuery = emailUserAdminQuery;
			_emailUserApplication = emailUserApplication;
		}

		public async Task<IActionResult> Index(int pageId =1,string q = "",int take=10)
		{
			return View(await _emailUserAdminQuery.GetEmailUsersForAdmin( new FilterParams(pageId,take,q)));
		}


		public IActionResult ChangeActivation(long id,string q ="", int pageId=1)
		{

			if (_emailUserApplication.ActivationChange(id))
			{
				SuccessAlert();
			}
			else
			{
				ErrorAlert(ErrorMessages.InternalServerError);
			}

			return RedirectToAction("Index", new { pageId = pageId, q = q });

		}
	}
}
