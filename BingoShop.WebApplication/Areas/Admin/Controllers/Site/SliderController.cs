using BingoShop.WebApplication.Utility;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Blogs1.Application.Contract.BlogCategoryService.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SliderApplication.Command;
using Site.Application.Contract.SliderApplication.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Site
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class SliderController : ControllerBase
	{
		private readonly ISliderQuery _SliderQuery;
		private readonly ISliderApplication _SliderApplication;

		public SliderController(ISliderQuery SliderQuery, ISliderApplication SliderApplication)
		{
			_SliderQuery = SliderQuery;
			_SliderApplication = SliderApplication;
		}

		public IActionResult Index()
		{
			var model =  _SliderQuery.GetAllForAdmin();

			return View(model);
		}

		public async Task<IActionResult> Create()
		{
			
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(CreateSlider model)
		{

			if (!ModelState.IsValid)  
				return View(model);
			

			var result = await _SliderApplication.Create(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("اسلاید ایجاد شد!");
				return View(model);
			}

			ErrorAlert(result.Message);
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(long id)
		{
			var model =  _SliderApplication.GetForEdit(id);
			if (model == null) return RedirectAndShowAlert(RedirectToAction("Index"),
					OperationResult.Error(ErrorMessages.InternalServerError));

			return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Edit(EditSlider model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _SliderApplication.Edit(model);
			if (result.Status == Status.Success)
			{
				result.Message = "اسلاید ویرایش شد!";
				return RedirectAndShowAlert(RedirectToAction("Index"), result);
			}

			ErrorAlert(result.Message);
			return View(model);

		}

		public async Task<bool> ChangeActivation(long id, int pageId = 1)
			=> await _SliderApplication.ActivationChange(id);

	}
}
