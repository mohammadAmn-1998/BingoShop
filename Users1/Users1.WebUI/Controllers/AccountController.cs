using System.Diagnostics.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Services;
using Shared.Application.Utility;
using Shared.Application.Utility.Validations;
using Shared.Domain.Enums;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Contract.UserService.Query;
using Users1.WebUI.Utility;

namespace Users1.WebUI.Controllers
{
	public class AccountController : ControllerBase
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
				SuccessAlert("رمز ورود به شما فرستاده شد!" +"\n"+"لطفا رمز شش رقمی ارسال شده را وارد کنید");
				return RedirectToAction("Login", new LoginUser
				{
					Mobile = model.Mobile,
					ReturnUrl = model.ReturnUrl,
					PassKey = ""
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
			return RedirectAndShowAlert(Redirect(model.ReturnUrl),new OperationResult(Status.Success,"شما وارد شدید!"));

		}

		public IActionResult LogOut(string returnUrl = "/")
		{
			return _authService.Logout() ? RedirectAndShowAlert(Redirect(returnUrl),new OperationResult(Status.Info,"شما از حساب کاربری خود خارج شدید!")) : Redirect("Home/Error");
		}

		public IActionResult Profile()
		{
			var userId = HttpContext.User.GetUserId();

			if (userId == 0) return Redirect($"/Home/Error");

			var model = _userQuery.GetUserBy(userId);

			return View(model);
		}

	}
}
