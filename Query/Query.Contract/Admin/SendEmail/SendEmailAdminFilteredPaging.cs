using Shared.Application.Models;
using Shared.Application.Utility;

namespace Query.Contract.Admin.SendEmail;

public class SendEmailAdminFilteredPaging : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<SendEmailAdminQueryModel> SendEmails { get; set; }

}