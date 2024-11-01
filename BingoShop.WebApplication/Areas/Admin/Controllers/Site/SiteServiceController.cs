﻿using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Application.Contract.SiteServiceApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_ادمین)]
	public class SiteServiceController : ControllerBase
	{
		private readonly ISiteServiceQuery _SiteServiceQuery;
		private readonly ISiteServiceApplication _SiteServiceApplication;

		public SiteServiceController(ISiteServiceQuery SiteServiceQuery, ISiteServiceApplication SiteServiceApplication)
		{
			_SiteServiceQuery = SiteServiceQuery;
			_SiteServiceApplication = SiteServiceApplication;
		}

		public async Task<IActionResult> Index(int pageId = 1, string q = "", int take = 10)
		{
			var model =  _SiteServiceQuery.GetAllForAdmin();

			return View(model);
		}

		public async Task<IActionResult> Create()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSiteService model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			
			var result = await _SiteServiceApplication.Create(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("سایت سرویس ایجاد شد!");
				return View(model);
			}

			ErrorAlert(result.Message);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			var model =  _SiteServiceApplication.GetForEdit(id);
			if (model == null) return RedirectAndShowAlert(RedirectToAction("Index"),
					OperationResult.Error(ErrorMessages.InternalServerError));

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditSiteService model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _SiteServiceApplication.Edit(model);
			if (result.Status == Status.Success)
			{
				result.Message = "سایت سرویس ویرایش شد!";
				return RedirectAndShowAlert(RedirectToAction("Index"), result);
			}

			ErrorAlert(result.Message);
			return View(model);
			
		}

		public async Task<bool> ChangeActivation(long id, int pageId = 1)
			=> await _SiteServiceApplication.ActivationChange(id);


	}
}
