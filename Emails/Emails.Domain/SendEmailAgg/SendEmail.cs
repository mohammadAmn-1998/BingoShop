using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Emails.Domain.SendEmailAgg
{
	public class SendEmail : BaseEntity<long>
	{

		public string Title { get; private set; }

		public string Text { get; private set; }

		public SendEmail(string title, string text)
		{
			Title = title;
			Text = text;
		}
	}
}
