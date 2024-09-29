using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Domain.EmailUserAgg;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Admin.EmailUser;
using Shared.Application.Models;
using Shared.Application.Utility;

namespace Query.Services.Services
{
	internal class EmailUserAdminQuery : IEmailUserAdminQuery
	{
		private IEmailUserRepository _emailUserRepository;

		public EmailUserAdminQuery(IEmailUserRepository emailUserRepository)
		{
			_emailUserRepository = emailUserRepository;
		}

		public async Task<EmailUserAdminFilteredPaging> GetEmailUsersForAdmin(FilterParams filterParams)
		{
			try
			{
				var result = _emailUserRepository.GetAsQueryable();

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					result = result?.Where(x => x.Email.ToLower().Contains(filterParams.Title.ToLower()));
				}

				EmailUserAdminFilteredPaging model = new();
				model.GetBasePagination(result,filterParams.PageId,filterParams.Take);
				model.FilterParams = filterParams;
				model.Emails = new();

				if(result != null)
					model.Emails = await result.Skip(model.Skip).Take(model.Take).OrderByDescending(x => x.CreateDate)
					.Select(x => new EmailUserAdminQueryModel
					{
						Id = x.Id,
						UserId = x.UserId,
						Email = x.Email,
						IsActive = x.Active,
						CreateDate = x.CreateDate.ConvertToPersianDate()
					}).ToListAsync();

				return model;


			}
			catch (Exception e)
			{
				return new();
			}
		}
	}
}
