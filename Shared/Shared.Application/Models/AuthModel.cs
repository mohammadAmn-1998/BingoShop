using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Models
{
	public class AuthModel
	{
		public AuthModel(long userId, string userUniqueKey, string mobile, string fullName)
		{
			UserId = userId;
			UserUniqueKey = userUniqueKey;
			Mobile = mobile;
			FullName = fullName;
		}

		public long UserId { get; set; }

		public string UserUniqueKey { get; set; }

		public string Mobile { get; set; }
		public string FullName { get; set; }



	}
}
