﻿using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Command;
using Users1.Application.Contract.RoleService.Query;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Contract.UserService.Query;
using Users1.WebUI.Utility;
using ControllerBase = Users1.WebUI.Controllers.ControllerBase;

namespace Users1.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_ادمین)]
	public class HomeController : ControllerBase
	{

		private readonly IUserQuery _userQuery;
		private readonly IUserService _userService;
		private readonly IRoleQuery _roleQuery;

		public HomeController(IUserQuery userQuery, IUserService userService, IRoleQuery roleQuery)
		{
			_userQuery = userQuery;
			_userService = userService;
			_roleQuery = roleQuery;
		}

		public IActionResult Index()
		{
			return View();
		}

		public  IActionResult Users(string q="",int pageId=1, int take=4)
		{

			var model = _userQuery.GetFilteredUsers(new()
			{
				PageId = pageId,
				Take = take,
				Title = q.Trim()
			});

			return View(model);
		}

		public IActionResult EditUser(long userId)
		{

			var user = _userService.GetForEditByAdmin(userId);

			return View(user);

		}

		[HttpPost]
		public async Task<IActionResult> EditUser(EditUserByAdmin model)
		{
			if (!ModelState.IsValid)
				return View(model);

			var result = await _userService.Edit(model);

			if (result.Status == Status.Success)
			  return RedirectAndShowAlert(Redirect("./"), result);
				
			ErrorAlert(result.Message);
			return View(model);

		}


		#region Handlers

		[Route("/Admin/Home/ChangeUserActivation/{userId}")]
		public async Task<JsonResult> ChangeUserActivation(long userId)
		{

			return await _userService.ActivationChange(userId)
				? Json(new { Success = true, Title = "انجام شد!" })
				: Json(new { Success = false, Title = "مشکلی به وجود آمد.لطفا در زمان دیگری تلاش کنید" });

		}

		[Route("/Admin/Home/ChangeUserBan/{userId}")]
		public async Task<JsonResult> ChangeUserBan(long userId)
		{

			return await _userService.BanChange(userId)
				? Json(new { Success = true, Title = "انجام شد!" })
				: Json(new { Success = false, Title = "مشکلی به وجود آمد.لطفا در زمان دیگری تلاش کنید" });

		}


		#endregion



	}
}
