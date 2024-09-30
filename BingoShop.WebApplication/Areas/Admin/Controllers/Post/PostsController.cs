using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using PostModule.Application.Contract.PostApplication;
using PostModule.Application.Contract.PostQuery;
using PostModule.Application.Contract.PostSettingApplication.Command;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.Post
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]
	public class PostsController : ControllerBase
	{
		private readonly IPostQuery _postQuery;
		private readonly IPostApplication _postApplication;
		private readonly IPostSettingApplication _postSettingApplication;

		public PostsController(IPostQuery postQuery, IPostApplication postApplication, IPostSettingApplication postSettingApplication)
		{
			_postQuery = postQuery;
			_postApplication = postApplication;
			_postSettingApplication = postSettingApplication;
		}

		public IActionResult Index() => View(_postQuery.GetAllPostsForAdmin());



		public IActionResult Create() => View();

		[HttpPost]
		public IActionResult Create(CreatePost model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _postApplication.Create(model);
			if (res.Status == Status.Success)
			{
				SuccessAlert("پست ایجاد شد!");
				return RedirectToAction("Index");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}

		public IActionResult Edit(int id) => View(_postApplication.GetForEdit(id));

		[HttpPost]
		public IActionResult Edit(int id, EditPost model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _postApplication.Edit(model);
			if (res.Status == Status.Success)
			{
				TempData["ok"] = true;
				return Redirect($"/Admin/Post/Edit/{id}");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
		public IActionResult Active(int id)
		{

			if (_postApplication.ActivationChange(id))
			{
				SuccessAlert();
			}
			else
			{
				ErrorAlert(ErrorMessages.InternalServerError);
			}

			return RedirectToAction("Index");
		}

		public IActionResult Inside(int id)
		{
			if (_postApplication.InsideCityChange(id))
			{
				SuccessAlert();
			}
			else
			{
				ErrorAlert(ErrorMessages.InternalServerError);
			}

			return RedirectToAction("Index");
		}

		public IActionResult Outside(int id)
		{
			if (_postApplication.OutSideCityChange(id))
			{
				SuccessAlert();
			}
			else
			{
				ErrorAlert(ErrorMessages.InternalServerError);
			}
			return RedirectToAction("Index");
		}


		public IActionResult Setting() => View(_postSettingApplication.GetForUbsert());
		[HttpPost]
		public IActionResult Setting(UbsertPostSetting model)
		{
			if (!ModelState.IsValid) return View(model);
			var res = _postSettingApplication.Ubsert(model);
			if (res.Status == Status.Success)
			{
				SuccessAlert();
				return Redirect("/Admin/Post/Setting/");
			}
			ModelState.AddModelError(res.ModelName, res.Message);
			return View(model);
		}
	}
}
