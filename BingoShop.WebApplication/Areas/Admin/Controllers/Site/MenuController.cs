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
	[RequiredPermission(UserPermission.پنل_ادمین)]
	public class MenuController : ControllerBase
	{
		private readonly IMenuQuery _MenuQuery;
		private readonly IMenuApplication _MenuApplication;

		public MenuController(IMenuQuery MenuQuery, IMenuApplication MenuApplication)
		{
			_MenuQuery = MenuQuery;
			_MenuApplication = MenuApplication;
		}

		public async Task<IActionResult> Index(int id = 0)
		{
			var model =  await _MenuQuery.GetForAdmin(id);

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

		public async Task<IActionResult> CreateSub(int id) => View(await _MenuApplication.GetForCreate(id));
		[HttpPost]
		public async Task<IActionResult> CreateSub(int id, CreateSubMenu model)
		{
			if (!ModelState.IsValid) return View(model);
			var res =await _MenuApplication.CreateSub(model);
			if (res.Status == Status.Success)
			{
				SuccessAlert($"منو با نام {model.Title} ایجاد شد!");
				return Redirect($"/Admin/Menu/Index/{id}");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
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
				result.Message = $"منوی {model.Title} ویرایش شد!";
				return RedirectAndShowAlert(RedirectToAction("Index"), result);
			}

			ErrorAlert(result.Message);
			return View(model);

		}

		public async Task<bool> ChangeActivation(long id, int pageId = 1)
			=> await _MenuApplication.ActivationChange(id);



	}
}
