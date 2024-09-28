using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emails.Application.Contract.SendEmailApplication.Query
{
	public interface ISendEmailQuery
	{
		List<SendEmailQueryModel> GetEmailSendsFoeAdmin();
		SendEmailDetailQueryModel GetSendEmailDetailForAdmin(int id);
	}
}
