using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Application.Contract.SendEmailApplication.Query;
using Emails.Query.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Emails.Query.Bootstrapper
{
	public class EmailQueryBootstrapper
	{

		public static void Config(IServiceCollection services)
		{

			services.AddTransient<ISendEmailQuery, SendEmailQuery>();

		}

	}
}
