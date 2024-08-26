using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Utility
{
	public static class RandomGenerator
	{

		public static string GenerateRandomUserTwoStepVerificationPassKey()
		{

			var random = new Random().Next(100000, 999999);
			return random.ToString();

		}

		public static string GenerateUserUniqueCode()
		{
			var guid = Guid.NewGuid().ToString();
			return guid;
		}
	}
}
