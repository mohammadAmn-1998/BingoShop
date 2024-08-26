using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Application.Utility.Validations;
using Shared.Domain.Enums;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Contract.UserService.Query;

namespace Users1.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly IUserQuery _userQuery;
		private readonly IAuthService _authService;
		public AccountController(IUserService userService, IUserQuery userQuery, IAuthService authService)
		{
			_userService = userService;
			_userQuery = userQuery;
			_authService = authService;
		}

		public IActionResult Register(string returnUrl = "/")
		{
			return View(new RegisterUser()
			{
				ReturnUrl = returnUrl
			});
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterUser model)
		{

			if(!ModelState.IsValid) 
				return View(model);

			var ok = await _userService.Register(model);
			if (!ok)
			{
				ModelState.AddModelError(nameof(model.Mobile),ErrorMessages.InternalServerError);
				return View(model);
			}
			
			
			var user = _userQuery.GetUserByMobile(model.Mobile.Trim());
			if (user != null)
			{
				TempData["Success"] = true;
				return RedirectToAction("Login", new LoginUser
				{
					Mobile = model.Mobile,
					ReturnUrl = model.ReturnUrl
				});
			}
			
			ModelState.AddModelError(nameof(model.Mobile),ErrorMessages.InternalServerError);
			return View(model);
		}

		

		public IActionResult Login(LoginUser model)
		{
			return View(model);
		}

		[HttpPost]
		public IActionResult LoginUser(LoginUser model)
		{
			if (!ModelState.IsValid)
				return View("Login", model);

			var result = _userService.Login(model);
			if (result.Status != Status.Success)
			{
				ModelState.AddModelError(result.ModelName!,result.Message!);
				return View("Login", model);
			}

			TempData["SuccessLogin"] = true;
			return Redirect(model.ReturnUrl);

		}

		public IActionResult LogOut()
		{
			return _authService.Logout() ? Redirect("/") : Redirect("Home/Error");
		}

	}
}
