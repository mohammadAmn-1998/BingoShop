using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Domain.EmailUserAgg;
using Emails.Domain.MessageUserAgg;
using Emails.Domain.SendEmailAgg;
using Emails.Infrastructure.Context;
using Emails.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Emails.Infrastructure.Bootstrapper
{
	public class EmailInfrastructureBootstrapper
	{

		public static void Config(IServiceCollection services, string connectionString)
		{

			services.AddDbContext<EmailContext>(options =>
			{
				options.UseSqlServer(connectionString);
			});

			

			services.AddTransient<IEmailUserRepository, EmailUserRepository>();
			services.AddTransient<IMessageUserRepository, MessageUserRepository>();
			services.AddTransient<ISendEmailRepository, SendEmailRepository>();

		}

	}
}
