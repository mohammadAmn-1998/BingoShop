using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Application.Contract.SiteSettingApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class SiteSettingController : ControllerBase
	{
		private readonly ISiteSettingQuery _SiteSettingQuery;
		private readonly ISiteSettingApplication _SiteSettingApplication;

		public SiteSettingController(ISiteSettingQuery SiteSettingQuery, ISiteSettingApplication SiteSettingApplication)
		{
			_SiteSettingQuery = SiteSettingQuery;
			_SiteSettingApplication = SiteSettingApplication;
		}

		public async Task<IActionResult> Index()
		{

			var model = await _SiteSettingApplication.GetForUbsert();
			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Index(UbsertSiteSetting model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			

			var result = await _SiteSettingApplication.Ubsert(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("تنظیمات سایت  ایجاد شد!");
				return View(model);
			}

			ErrorAlert(result.Message);
			return View(model);
		}


	}
}
