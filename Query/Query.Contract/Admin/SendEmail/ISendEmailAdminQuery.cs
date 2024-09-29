using Shared.Application.Models;

namespace Query.Contract.Admin.SendEmail;

public interface ISendEmailAdminQuery
{
	Task<SendEmailAdminFilteredPaging> GetSendEmailsForAdmin(FilterParams  filterParams);
}