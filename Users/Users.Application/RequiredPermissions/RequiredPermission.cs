
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Domain.Enums;
using Users.Application.Services.Interfaces;


namespace Users.Application.RequiredPermissions
{
	public class RequiredPermission : AuthorizeAttribute, IAuthorizationFilter
	{
		private readonly UserPermission _permission;
		private  IRoleService? _roleService;

		public RequiredPermission(UserPermission permission)
		{
			_permission = permission;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{

			try
			{

				_roleService = context.HttpContext.RequestServices.GetService(typeof(IRoleService)) as IRoleService;

				if (_roleService == null)
				{
					context.Result = new RedirectResult("/Error");
					return;
				}
				
				if (context.HttpContext.User.Claims.Any())
				{

					var id = context.HttpContext.User.Claims.Single(x => x.Type == "user_id").Value;
					var userId = int.Parse(id);

					var ok = _roleService.CheckPermission(userId,_permission);

					if (!ok)
						context.Result = new RedirectResult("/not-permitted");
				}
				else
				{
					context.Result = new RedirectResult("/sign-in");
				}

			}
			catch (Exception e)
			{
				context.Result = new RedirectResult("/Error");
			}

		}
	}
}
