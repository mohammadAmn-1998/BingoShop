using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Application.Contract.MessageUserApplication.Command;
using Emails.Application.Contract.SendEmailApplication.Command;
using Emails.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Emails.Application.Bootstrapper
{
	public class EmailApplicationBootstrapper
	{

		public static void Config(IServiceCollection services)
		{

			services.AddTransient<ISendEmailApplication, SendEmailApplication>();
			services.AddTransient<IEmailUserApplication, EmailUserApplication>();
			services.AddTransient<IMessageUserApplication, MessageUserApplication>();

		}

	}
}
