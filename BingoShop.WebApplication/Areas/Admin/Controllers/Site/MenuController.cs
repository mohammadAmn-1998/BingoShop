using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.MenuApplication.Command;
using Site.Application.Contract.MenuApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class MenuController : ControllerBase
	{
		private readonly IMenuQuery _MenuQuery;
		private readonly IMenuApplication _MenuApplication;

		public MenuController(IMenuQuery MenuQuery, IMenuApplication MenuApplication)
		{
			_MenuQuery = MenuQuery;
			_MenuApplication = MenuApplication;
		}

		public async Task<IActionResult> Index(int parentId = 0)
		{
			var model =  await _MenuQuery.GetForAdmin(parentId);

			return View(model);
		}

		public async Task<IActionResult> Create()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateMenu model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			

			var result = await _MenuApplication.Create(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("منو ایجاد شد!");
				return View(model);
			}

			ErrorAlert(result.Message);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			var model =  _MenuApplication.GetForEdit(id);
			if (model == null) return RedirectAndShowAlert(RedirectToAction("Index"),
					OperationResult.Error(ErrorMessages.InternalServerError));

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditMenu model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _MenuApplication.Edit(model);
			if (result.Status == Status.Success)
			{
				result.Message = "بنر ویرایش شد!";
				return RedirectAndShowAlert(RedirectToAction("Index"), result);
			}

			ErrorAlert(result.Message);
			return View(model);

		}

		public async Task<IActionResult> ChangeActivation(long menuId, int pageId = 1)
		{
			if (await _MenuApplication.ActivationChange(menuId))
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
