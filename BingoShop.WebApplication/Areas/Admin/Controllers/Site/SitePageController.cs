using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SitePageApplication.Command;
using Site.Application.Contract.SitePageApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class SitePageController : ControllerBase
	{
		private readonly ISitePageQuery _SitePageQuery;
		private readonly ISitePageApplication _SitePageApplication;

		public SitePageController(ISitePageQuery SitePageQuery, ISitePageApplication SitePageApplication)
		{
			_SitePageQuery = SitePageQuery;
			_SitePageApplication = SitePageApplication;
		}

		public async Task<IActionResult> Index(int pageId = 1, string q = "", int take = 2)
		{
			var model =  _SitePageQuery.GetAllForAdmin();

			return View(model);
		}

		public async Task<IActionResult> Create()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSitePage model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			

			var result = await _SitePageApplication.Create(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("پیج سایت ایجاد شد!");
				return View(model);
			}

			ErrorAlert(result.Message);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			var model =  _SitePageApplication.GetForEdit(id);
			if (model == null) return RedirectAndShowAlert(RedirectToAction("Index"),
					OperationResult.Error(ErrorMessages.InternalServerError));

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditSitePage model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _SitePageApplication.Edit(model);
			if (result.Status == Status.Success)
			{
				result.Message = "بنر ویرایش شد!";
				return RedirectAndShowAlert(RedirectToAction("Index"), result);
			}

			ErrorAlert(result.Message);
			return View(model);

		}

		public async Task<IActionResult> ChangeActivation(long sitePageId, int pageId = 1)
		{
			if (await _SitePageApplication.ActivationChange(sitePageId))
			{
				SuccessAlert();
			}
			else
			{
				ErrorAlert(ErrorMessages.InternalServerError);
			}

			return RedirectToAction("Index", new { pageId });
		}

	}
}
