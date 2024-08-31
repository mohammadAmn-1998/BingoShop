using System.Security.Claims;

namespace Users1.WebUI.Utility
{
	public static class UserHelper
	{


		public static long GetUserId(this ClaimsPrincipal? principal)
		{

			try
			{
				var userId = principal?.Claims.Single(claim => claim.Type == "user_id").Value;

				return userId != null ? long.Parse(userId) : 0;
			}
			catch (Exception e)
			{
				return 0;
			}

		}
	}
}
