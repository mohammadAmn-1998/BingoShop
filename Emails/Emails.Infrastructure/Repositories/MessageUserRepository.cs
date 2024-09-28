using Emails.Domain.MessageUserAgg;
using Emails.Infrastructure.Context;
using Shared.Infrastructure;
using Shared.Infrastructure.BaseRepository;

namespace Emails.Infrastructure.Services;

internal class MessageUserRepository : Repository<long, MessageUser>, IMessageUserRepository
{
	private readonly EmailContext _context;

	public MessageUserRepository(EmailContext context) : base(context)
	{
		_context = context;
	}
}
