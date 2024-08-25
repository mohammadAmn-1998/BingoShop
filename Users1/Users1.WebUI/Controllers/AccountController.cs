using Microsoft.AspNetCore.Mvc;
using Shared.Application.Utility;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Contract.UserService.Query;

namespace Users1.WebUI.Controllers
{
	public class AccountController : Controller
	{
		private readonly IUserService _userService;
		private readonly IUserQuery _userQuery;

		public AccountController(IUserService userService, IUserQuery userQuery)
		{
			_userService = userService;
			_userQuery = userQuery;
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
				return RedirectToAction("Activation", new { id = user.userId });
			}
			
			ModelState.AddModelError(nameof(model.Mobile),ErrorMessages.InternalServerError);
			return View(model);
		}

		public IActionResult Activation(long id)
		{
			return View();
		}
	}
}
