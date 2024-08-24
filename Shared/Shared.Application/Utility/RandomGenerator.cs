using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public static class RandomGenerator
	{

		public static string GenerateRandomUserActiveKey()
		{

			var random = new Random().Next(10000, 99999);
			return random.ToString();

		}

		public static string GenerateUserUniqueCode()
		{
			var guid = Guid.NewGuid().ToString();
			return guid;
		}
	}
}
