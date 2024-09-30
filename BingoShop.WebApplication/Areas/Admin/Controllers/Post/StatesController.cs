using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.StateApplication;
using PostModule.Application.Contract.StateQuery;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Post
{
	public class StatesController : ControllerBase
	{
		private readonly IStateQuery _stateQuery;
		private readonly IStateApplication _stateApplication;


		public StatesController(IStateQuery stateQuery, IStateApplication stateApplication)
		{
			_stateQuery = stateQuery;
			_stateApplication = stateApplication;
		}

		public IActionResult Index()
		 =>  View(_stateQuery.GetStatesForAdmin());

		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(CreateStateModel model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = _stateApplication.Create(model);

			if (result.Status == Status.Success)
				return RedirectAndShowAlert(RedirectToAction("Index"), result);

			ModelState.AddModelError(result.ModelName,result.Message);
			return View(model);
		}

		public IActionResult Edit(int stateId)
		{
			var model = _stateApplication.GetStateForEdit(stateId);
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditStateModel model)
		{

			if (!ModelState.IsValid)
				return View(model);

			var result = _stateApplication.Edit(model);

			if (result.Status == Status.Success)
				return RedirectAndShowAlert(RedirectToAction("Index"), result);

			ModelState.AddModelError(result.ModelName, result.Message);
			return View(model);

		}


		public IActionResult ChangeCloseStates(int id,List<int> closeStates)
		{


			if (closeStates.Count() < 1)
			{
				ErrorAlert(" استان های همجوار باید حداقل یکی انتخاب شده باشد از");
				return Redirect($"/Admin/Cities/Index/{id}");
			}
			if (_stateApplication.ChangeStateClose(id, closeStates))
			{
				SuccessAlert("استان های همجوار ثبت شدند!");
				return Redirect($"/Admin/Cities/Index/{id}");
			}
			else
			{
				ErrorAlert(ErrorMessages.InternalServerError);
				return Redirect($"/Admin/Cities/Index/{id}");
			}

		}
	}
}
