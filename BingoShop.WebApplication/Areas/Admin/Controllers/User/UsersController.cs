using BingoShop.WebApplication.Utility;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Query;
using Users1.Application.Contract.UserService.Command;
using Users1.Application.Contract.UserService.Query;
using ControllerBase = BingoShop.WebApplication.Controllers.ControllerBase;

namespace BingoShop.WebApplication.Areas.Admin.Controllers.User
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.پنل_کاربران)]
	public class UsersController : ControllerBase
	{
		private readonly IUserQuery _userQuery;
		private readonly IUserService _userService;
		private readonly IRoleQuery _roleQuery;

		public UsersController(IUserQuery userQuery, IUserService userService, IRoleQuery roleQuery)
		{
			_userQuery = userQuery;
			_userService = userService;
			_roleQuery = roleQuery;
		}

		

		public IActionResult Index(string q = "", int pageId = 1, int take = 4)
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

		[Route("/Admin/Users/ChangeActivation/{userId}")]
		public async Task<bool> ChangeActivation(long userId)
			=> await _userService.ActivationChange(userId);


		

		[Route("/Admin/Users/ChangeBan/{userId}")]
		public async Task<bool> ChangeBan(long userId)
			=> await _userService.BanChange(userId);


		#endregion

	}
}
