using Shared.Domain.SeedWorks.Base;

namespace Emails.Domain.EmailUserAgg;

public interface IEmailUserRepository  : IRepository<long,EmailUser>
{
	
		bool CreateList(List<EmailUser> emailUsers);
		EmailUser GetByEmail(string email);
	
}