﻿using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shared.Application.Models;
using Shared.Application.Services;

namespace BingoShop.WebApplication.Services
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
					new Claim("MobileNumber", model.Mobile),
					new Claim("user_id", model.UserId.ToString()),
					new Claim(ClaimTypes.Name, model.FullName),
					new Claim("user_avatar",model.Avatar)
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
				var mobile = HttpContext?.User.Claims.Single(x => x.Type == "MobileNumber").Value;

				return mobile ?? "";

			}
			catch (Exception e)
			{
				return "";
			}
		}

		public string GetUserFullName()
		{
			try
			{
				var fullName = HttpContext?.User.Claims.Single(x => x.Type == ClaimTypes.Name).Value;

				return fullName ?? "";

			}
			catch (Exception e)
			{
				return "";
			}
		}

		public long GetUserId()
		{
			try
			{
				var userId = HttpContext?.User.Claims.Single(x => x.Type == "user_id").Value;

				if (userId == null)
					return 0;

				return long.Parse(userId!);

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
				if (HttpContext?.User.Identity != null && HttpContext != null && HttpContext.User.Identity.IsAuthenticated)
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

		public string GetUserAvatar()
		{
			try
			{
				var userAvatar = HttpContext?.User.Claims.Single(x => x.Type == "user_avatar").Value;

				return userAvatar ?? "Default.png";

			}
			catch (Exception e)
			{
				return "Default.png";
			}
		}
	}
}
