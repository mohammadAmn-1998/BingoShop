using Shared.Application.Models;
using Shared.Application.Utility;

namespace Query.Contract.Admin.EmailUser;

public class EmailUserAdminFilteredPaging : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<EmailUserAdminQueryModel> Emails { get; set; }

}