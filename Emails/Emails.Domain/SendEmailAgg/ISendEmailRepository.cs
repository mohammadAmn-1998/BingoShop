using Emails.Domain.EmailUserAgg;
using Shared.Domain.SeedWorks.Base;

namespace Emails.Domain.SendEmailAgg;

public interface ISendEmailRepository : IRepository<long, SendEmail>
{
	
}