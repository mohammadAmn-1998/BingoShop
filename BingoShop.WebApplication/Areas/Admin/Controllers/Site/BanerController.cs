using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.BanerApplication.Command;
using Site.Application.Contract.BanerApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_بنرها)]
	public class BanerController : ControllerBase
	{
		private readonly IBanerQuery _banerQuery;
		private readonly IBanerApplication _banerApplication;

		public BanerController(IBanerQuery banerQuery, IBanerApplication banerApplication)
		{
			_banerQuery = banerQuery;
			_banerApplication = banerApplication;
		}

		public async Task<IActionResult> Index() 
			=> View(_banerQuery.GetAllForAdmin());

		
		public  IActionResult Create()
			=>  View();
		

		[HttpPost]
		public async Task<IActionResult> Create(CreateBaner model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			

			var result = await _banerApplication.Create(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("بنر ایجاد شد!");
				return RedirectToAction("Index");
			}

			ErrorAlert(result.Message);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			var model =  _banerApplication.GetForEdit(id);
			if (model == null) return RedirectAndShowAlert(RedirectToAction("Index"),
					OperationResult.Error(ErrorMessages.InternalServerError));

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditBaner model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _banerApplication.Edit(model);
			if (result.Status == Status.Success)
			{
				result.Message = "بنر ویرایش شد!";
				return RedirectAndShowAlert(RedirectToAction("Index"), result);
			}

			ErrorAlert(result.Message);
			return View(model);

		}
		[Route("Admin/Baner/ChangeActivation/{id}")]
		public async Task<bool> ChangeActivation(long id)
		{

			return await _banerApplication.ActivationChange(id);
		}

	}
}
