using Emails.Domain.EmailUserAgg;
using Shared.Domain.SeedWorks.Base;

namespace Emails.Domain.MessageUserAgg;

public interface IMessageUserRepository : IRepository<long, MessageUser>
{
	
}