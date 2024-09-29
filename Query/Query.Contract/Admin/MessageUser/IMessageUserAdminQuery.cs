using Shared.Application.Models;
using Shared.Domain.Enums;

namespace Query.Contract.Admin.MessageUser;

public interface IMessageUserAdminQuery
{
	
	Task<MessageUserAdminFilteredPaging> GetMessageUsersForAdmin(FilterParams filterParams,
		MessageStatus? messageStatus);

	Task<MessageUserAdminQueryModel?> GetMessageUserDetailForAdmin(long id);

}