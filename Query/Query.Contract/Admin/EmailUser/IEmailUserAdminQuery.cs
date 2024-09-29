using Shared.Application.Models;

namespace Query.Contract.Admin.EmailUser;

public interface IEmailUserAdminQuery
{

    Task<EmailUserAdminFilteredPaging> GetEmailUsersForAdmin(FilterParams  filterParams);

}