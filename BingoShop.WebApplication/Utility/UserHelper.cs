using System.Security.Claims;

namespace BingoShop.WebApplication.Utility
{
	public static class UsersHelper
	{


		public static long GetUserId( ClaimsPrincipal? principal)
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
