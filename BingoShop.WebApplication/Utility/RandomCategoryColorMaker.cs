namespace BingoShop.WebApplication.Utility
{
	public static class RandomCategoryColorMaker
	{

		public static string GenerateRandomColor()
		{

			var colors = new string[] { "violet", "purple", "blue", "green" };

			return colors[Random.Shared.Next(0, colors.Length)];

		}

	}
}
