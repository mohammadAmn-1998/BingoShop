using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Domain.SendEmailAgg;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Admin.SendEmail;
using Shared.Application.Models;
using Shared.Application.Utility;

namespace Query.Services.Services
{
	internal class SendEmailAdminQuery : ISendEmailAdminQuery
	{
		private ISendEmailRepository _sendEmailRepository;

		public SendEmailAdminQuery(ISendEmailRepository sendEmailRepository)
		{
			_sendEmailRepository = sendEmailRepository;
		}

		public async Task<SendEmailAdminFilteredPaging> GetSendEmailsForAdmin(FilterParams filterParams)
		{
			try
			{
				var result = _sendEmailRepository.GetAsQueryable();

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					result = result?.Where(x => x.Title.Trim().ToLower().Contains(filterParams.Title.Trim().ToLower()));
				}

				SendEmailAdminFilteredPaging model = new();
				model.GetBasePagination(result,filterParams.PageId,filterParams.Take);
				model.FilterParams = filterParams;
				model.SendEmails = new();

				if (result != null)
					model.SendEmails = await result.Skip(model.Skip).Take(model.Take)
					.OrderByDescending(x => x.CreateDate)
					.Select(x => new SendEmailAdminQueryModel
					{
						Id = x.Id,
						Title = x.Title,
						Text = x.Text,
						CreateDate = x.CreateDate.ConvertToPersianDate()
					}).ToListAsync();

				return model;
			}
			catch (Exception e)
			{
				return new();
			}
		}

		public SendEmailAdminQueryModel GetSendEmailDetailForAdmin(long id)
		{
			try
			{
				var result = _sendEmailRepository.GetById(id);
				if (result == null)
					throw new NullReferenceException();

				return new()
				{
					CreateDate = result.CreateDate.ConvertToPersianDate(),
					Id = result.Id,
					Text = result.Text,
					Title = result.Title

				};
			}
			catch
			{
				return null;
			}
		}
	}
}
