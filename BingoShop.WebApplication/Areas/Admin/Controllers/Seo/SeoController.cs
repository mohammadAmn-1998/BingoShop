using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using Query.Contract.Admin.Seo;
using Seos.Application.Contract;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Seo
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_سئو)]
	public class SeoController : ControllerBase
	{
		private readonly ISeoApplication _seoApplication;
		private readonly ISeoAdminQuery _seoAdminQuery;

		public SeoController(ISeoApplication seoApplication, ISeoAdminQuery seoAdminQuery)
		{
			_seoApplication = seoApplication;
			_seoAdminQuery = seoAdminQuery;
		}
		[Route("/Admin/Seo/{id}/{where}")]
		public async Task<IActionResult> Index(long id, WhereSeo where)
		{
			ViewData["Title"] =await _seoAdminQuery.GetAdminSeoTitle(where, id);
			var model = _seoApplication.GetSeoForEdit(id, where);
			return View(model);
		}

		[HttpPost]
		[Route("/Admin/Seo/{id}/{where}")]
		public async Task<IActionResult> Index(long id,WhereSeo where, CreateSeo model)
		{
			ViewData["Title"] =await _seoAdminQuery.GetAdminSeoTitle(model.Where, model.OwnerId);
			if (!ModelState.IsValid) return View(model);
			var res =await _seoApplication.UbsertSeo(model);
			if (res)
			{
				SuccessAlert();
				return Redirect($"/Admin/Seo/{model.OwnerId}/{model.Where}");
			}
			else
			{
				ErrorAlert();
				return Redirect($"/Admin/Seo/{model.OwnerId}/{model.Where}");
			}
		}
	}
}
