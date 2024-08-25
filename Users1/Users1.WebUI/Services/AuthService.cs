using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Application.Models;
using Shared.Application.Services;

namespace Users1.WebUI.Services
{
	internal class AuthService : IAuthService
	{
		private readonly IHttpContextAccessor _contextAccessor;

		private HttpContext? HttpContext => _contextAccessor.HttpContext;

		public AuthService(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public bool Login(AuthModel model)
		{

			try
			{
				var claims = new List<Claim>()
				{
					new Claim(ClaimTypes.NameIdentifier, model.UserUniqueKey),
					new Claim(ClaimTypes.Name, model.Mobile),
					new Claim("user_id", model.UserId.ToString())
				};
				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);
				var properties = new AuthenticationProperties()
				{
					IsPersistent = true,
					AllowRefresh = true,
				};

				HttpContext!.SignInAsync(principal, properties);
				return true;
			}
			catch (Exception e)
			{
				return false;
			}

		}

		public string GetUserUniqueKey()
		{
			try
			{
				var userUniqueKey = HttpContext?.User.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;

				return userUniqueKey ?? "";

			}
			catch (Exception e)
			{
				return "";
			}
		}

		public string GetUserMobile()
		{
			try
			{
				var mobile = HttpContext?.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;

				return mobile ?? "";

			}
			catch (Exception e)
			{
				return "";
			}
		}

		public int GetUserId()
		{
			try
			{
				var userId = HttpContext?.User.Claims.Single(x => x.Type == "user_id").Value;

				return int.Parse(userId!);

			}
			catch (Exception e)
			{
				return 0;
			}
		}

		public bool Logout()
		{
			try
			{
				if (HttpContext.User.Identity.IsAuthenticated)
				{
					HttpContext.SignOutAsync();
					return true;

				}
				return false;
			}
			catch (Exception e)
			{

				return false;
			}

		}
	}
}
