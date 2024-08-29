using Microsoft.AspNetCore.Mvc;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Command;
using Users1.Application.Contract.RoleService.Query;
using ControllerBase = Users1.WebUI.Controllers.ControllerBase;

namespace Users1.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class RolesController : ControllerBase
	{

		private readonly IRoleService _roleService;
		private readonly IRoleQuery _roleQuery;
		
		public RolesController(IRoleService roleService, IRoleQuery roleQuery)
		{
			_roleService = roleService;
			_roleQuery = roleQuery;
		}

		public IActionResult Index()
		{
			return View();
		}

		public async Task<IActionResult> GetRolesForAddRoleToUser(long userId)
		{

			var roles = await _roleQuery.GetRoles();

			CreateUserRole model = new()
			{
				userId = userId,
				Roles = roles,
			};

			return PartialView("Modals/_AddUserRoleModal", model);

		}

		[HttpPost]
		public async Task<IActionResult> AddUserRole(CreateUserRole model)
		{

			if (model.userId == 0 || model.roleId == 0)
				return RedirectAndShowAlert(Redirect("/Admin/Home/Users"),
					new OperationResult(Status.InternalServerError, ErrorMessages.InternalServerError));

			var result = await _roleService.AddRoleToUser(model.userId, model.roleId);

				return RedirectAndShowAlert(Redirect("/Admin/Home/Users"), result);

			
		}

		public async Task<JsonResult> DeleteUserRole(long userId, long roleId)
		{

			return await _roleService.DeleteUserRole(userId, roleId)
				? Json(new { Success = true, Title = "نقش کاربر حذف شد!" })
				: Json(new { Success = false,Title = ErrorMessages.InternalServerError });
		}

	}
}
