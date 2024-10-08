using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.StateApplication;
using PostModule.Application.Contract.StateQuery;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Post
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_پست_ها)]
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
		{
			var model = _stateQuery.GetStatesForAdmin();

			foreach (var state in model)
			{
				state.CloseStates = _stateQuery.GetCloseStateTitlesByCloseStateIds(state.CloseStates.Trim());
			}

			return  View(model);
		}
		

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

		public IActionResult Edit(int id)
		{
			var model = _stateApplication.GetStateForEdit(id);
			return View(model);
		}

		[HttpPost]
		public IActionResult Edit(EditStateModel model)
		{

			if (!ModelState.IsValid)
				return View(model);

			if (model.CloseStates == null || !model.CloseStates.Any())
			{
				ErrorAlert(" استان های همجوار باید حداقل یکی انتخاب شده باشد از");
				return View(model);
			}

			var result = _stateApplication.Edit(model);

			if (result.Status == Status.Success)
				return RedirectAndShowAlert(RedirectToAction("Index"), result);

			
			ModelState.AddModelError(result.ModelName, result.Message);
			return View(model);

		}

		// [HttpPost]
		// public IActionResult ChangeCloseStates(EditStateModel model)
		// {
		//
		// 	if (!ModelState.IsValid)
		// 		return View("Edit",model);
		//
		// 	if (model.CloseStates == null || !model.CloseStates.Any())
		// 	{
		// 		ErrorAlert(" استان های همجوار باید حداقل یکی انتخاب شده باشد از");
		// 		return View("Edit", model);
		// 	}
		//
		// 	var ok = _stateApplication.ChangeStateClose(model.Id,model.CloseStates);
		//
		// 	if(ok)
		// 		return RedirectAndShowAlert(RedirectToAction("Edit",new{id= model.Id}), OperationResult.Success());
		//
		//
		// 	return RedirectAndShowAlert(RedirectToAction("Edit",new{id= model.Id}), OperationResult.Error());
		//
		// }


		// public IActionResult ChangeCloseStates(int id,List<int> closeStates)
		// {
		//
		//
		// 	if (closeStates.Count() < 1)
		// 	{
		// 		ErrorAlert(" استان های همجوار باید حداقل یکی انتخاب شده باشد از");
		// 		return Redirect($"/Admin/Cities/Index/{id}");
		// 	}
		// 	if (_stateApplication.ChangeStateClose(id, closeStates))
		// 	{
		// 		SuccessAlert("استان های همجوار ثبت شدند!");
		// 		return Redirect($"/Admin/Cities/Index/{id}");
		// 	}
		// 	else
		// 	{
		// 		ErrorAlert(ErrorMessages.InternalServerError);
		// 		return Redirect($"/Admin/Cities/Index/{id}");
		// 	}
		//
		// }
	}
}
