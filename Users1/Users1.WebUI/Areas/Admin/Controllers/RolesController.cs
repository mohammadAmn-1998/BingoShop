using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Users1.Application.Contract.RoleService.Command;
using Users1.Application.Contract.RoleService.Query;
using Users1.WebUI.Utility;
using ControllerBase = Users1.WebUI.Controllers.ControllerBase;

namespace Users1.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[RequiredPermission(UserPermission.AdminPanel)]

	public class RolesController : ControllerBase
	{

		private readonly IRoleService _roleService;
		private readonly IRoleQuery _roleQuery;
		
		public RolesController(IRoleService roleService, IRoleQuery roleQuery)
		{
			_roleService = roleService;
			_roleQuery = roleQuery;
		}

		public async Task<IActionResult> Index()
		{
			var roles = await _roleQuery.GetRoles();
			return View(roles);
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

		public async Task<IActionResult> EditRole(long roleId)
		{
			var model = _roleService.GetRoleForEdit(roleId);

			return PartialView("Modals/_EditRoleModal",model);
		}
		[HttpPost]
		public async Task<IActionResult> EditRole(EditRole model)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.FirstOrDefault(x => x.ValidationState == ModelValidationState.Invalid)
					?.Errors.Select(x => x.ErrorMessage).ToList();
				var errorMessage = " ";

				foreach (var error in errors!)
				{
					errorMessage = errorMessage + Environment.NewLine + error;
				}
				ErrorAlert(errorMessage);
				return Redirect("/Admin/Roles/Index");
			}


			var result =await _roleService.EditRole(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("نقش ویرایش شد!");
				return Redirect("/Admin/Roles/Index");
			}
			
			ErrorAlert(result.Message);
			return Redirect("/Admin/Roles/Index");
		}

		public async Task<IActionResult> CreateRole()
		{
			return PartialView("Modals/_CreateRoleModal" );
		}

		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRole model)
		{
			if (!ModelState.IsValid)
			{
				var errors = ModelState.Values.FirstOrDefault(x => x.ValidationState == ModelValidationState.Invalid)
					?.Errors.Select(x => x.ErrorMessage).ToList();
				var errorMessage = " ";

				foreach (var error in errors!)
				{
					errorMessage = errorMessage +Environment.NewLine + error;
				}
				ErrorAlert(errorMessage);
				return Redirect("/Admin/Roles/Index");
			}

			var result = await _roleService.CreateRole(model);

			if (result.Status == Status.Success)
			{
				SuccessAlert("نقش ایجاد شد!");
				return Redirect("/Admin/Roles/Index");
			}

			ErrorAlert(result.Message);
			return Redirect("/Admin/Roles/Index");
		}


		#region Handlers

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

		public async Task<JsonResult> DeleteUserRole(long userId, long roleId)
		{

			return await _roleService.DeleteUserRole(userId, roleId)
				? Json(new { Success = true, Title = "نقش کاربر حذف شد!" })
				: Json(new { Success = false, Title = ErrorMessages.InternalServerError });
		}


		#endregion

	}
}
