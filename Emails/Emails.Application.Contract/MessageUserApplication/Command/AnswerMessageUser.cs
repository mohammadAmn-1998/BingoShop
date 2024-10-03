using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.Enums;

namespace Emails.Application.Contract.MessageUserApplication.Command
{
	public class AnswerMessageUser
	{
		public long Id { get; set; }

		public MessageStatus Status { get; set; }

		public string AnswerEmail { get; set; }

		public string AnswerSms { get; set; }

	}
}
