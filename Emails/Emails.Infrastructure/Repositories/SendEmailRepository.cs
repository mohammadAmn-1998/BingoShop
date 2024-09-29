using Emails.Domain.SendEmailAgg;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Infrastructure.Context;
using Shared.Infrastructure.BaseRepository;

namespace Emails.Infrastructure.Repositories;

internal class SendEmailRepository : Repository<long, SendEmail>, ISendEmailRepository
{
	private readonly EmailContext _context;

	public SendEmailRepository(EmailContext context) : base(context)
	{
		_context = context;
	}
}
