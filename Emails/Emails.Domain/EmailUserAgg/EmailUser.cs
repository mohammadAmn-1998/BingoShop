using Shared.Domain.SeedWorks.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Domain.EmailUserAgg
{
	public class EmailUser : BaseEntityActive<long>
	{

		public long UserId { get; private set; }
		public string Email { get; private set; }
		public EmailUser(string email, long userId = 0)
		{
			UserId = userId;
			Email = email;
		}
		public void AddUserId(long userId)
		{
			UserId = userId;
		}
		public void EditEmail(string email)
		{
			Email = email;
		}

	}
}
