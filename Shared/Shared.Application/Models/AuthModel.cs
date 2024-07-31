using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Application.Models
{
	public class AuthModel
	{
		public AuthModel(int userId, string userUniqueKey, string mobile)
		{
			UserId = userId;
			UserUniqueKey = userUniqueKey;
			Mobile = mobile;
		}

		public int UserId { get; set; }

		public string UserUniqueKey { get; set; }

		public string Mobile { get; set; }



	}
}
