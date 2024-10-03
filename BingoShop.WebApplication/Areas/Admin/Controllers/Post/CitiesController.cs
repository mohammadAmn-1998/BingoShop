using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using PostModule.Application.Contract.CityApplication;
using PostModule.Application.Contract.StateQuery;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Post
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class CitiesController : ControllerBase
	{

		private readonly IStateQuery _stateQuery;
		private readonly ICityApplication _cityApplication;

		public CitiesController(IStateQuery stateQuery, ICityApplication cityApplication)
		{
			_stateQuery = stateQuery;
			_cityApplication = cityApplication;
		}

		public IActionResult Index(int id)
		{
			ViewData["StateId"] = id;
			ViewData["StateTitle"] = _stateQuery.GetStateTitle(id);
			return View(_cityApplication.GetAllForState(id));
		}

		public IActionResult Create(int stateId)
		{
			ViewData["StateTitle"] = _stateQuery.GetStateTitle(stateId);
			return View(new CreateCityModel()
			{
				StateId = stateId
			});
		}

		[HttpPost]
		public IActionResult Create(CreateCityModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var stateTitle =  _stateQuery.GetStateTitle(model.StateId);

			ViewData["Title"] = $" افزودن شهر به استان{stateTitle}";

			var result = _cityApplication.Create(model);

			if (result.Status == Status.Success)
				return RedirectAndShowAlert(RedirectToAction("Index", new { id = model.StateId }),
					OperationResult.Success("شهر جدید ثبت شد!"));

			ModelState.AddModelError(result.ModelName,result.Message);
			ErrorAlert(result.Message);
			return View(model);
		}


		public IActionResult Edit(int id)
		{
			var model = _cityApplication.GetCityForEdit(id);

			var stateTitle = _stateQuery.GetStateTitle(model.StateId);
			ViewData["Title"] = $" ویرایش شهر در استان{stateTitle}";

			return View(model);
		} 
		
		[HttpPost]
		public IActionResult Edit(EditCityModel model)
		{
			var stateTitle = _stateQuery.GetStateTitle(model.StateId);
			ViewData["Title"] = $" ویرایش شهر در استان{stateTitle}";

			if (!ModelState.IsValid)
				return View(model);

			var result = _cityApplication.Edit(model);

			if (result.Status == Status.Success)
				return RedirectAndShowAlert(RedirectToAction("Index", new { id = model.StateId }),
					OperationResult.Success("شهر  ویرایش شد!"));

			ModelState.AddModelError(result.ModelName, result.Message);
			ErrorAlert(result.Message);
			return View(model);
		}
		[Route("Admin/Cities/DeleteCity/{id}")]
		public IActionResult DeleteCity(int id)
		{

			ErrorAlert("متاسفیم شهر ها پاک نمیشوند مگر با بمب اتمی چیزی! با مدیر بانک اطلاعاتی تماس بگیرید!");
			return Redirect("/Admin/States/Index");

		}

		public async Task<bool> ChangeStatus(int id, CityStatus status)
			=> await _cityApplication.ChangeStatus(id, status);



	}
}
