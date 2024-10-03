using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emails.Domain.MessageUserAgg;
using Microsoft.EntityFrameworkCore;
using Query.Contract.Admin.EmailUser;
using Query.Contract.Admin.MessageUser;
using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Query.Services.Services
{
	internal class MessageUserAdminQuery : IMessageUserAdminQuery
	{
		private IMessageUserRepository _messageUserRepository;

		public MessageUserAdminQuery(IMessageUserRepository messageUserRepository)
		{
			_messageUserRepository = messageUserRepository;
		}

		public async Task<MessageUserAdminFilteredPaging> GetMessageUsersForAdmin(FilterParams filterParams, MessageStatus? messageStatus)
		{

			try
			{

				var result = _messageUserRepository.GetAsQueryable();

				if (!string.IsNullOrEmpty(filterParams.Title))
				{
					result = result?.Where(x => x.Subject.ToLower().Contains(filterParams.Title.ToLower()) || x.Message.ToLower().Contains(filterParams.Title.ToLower()) || x.FullName.ToLower().Contains(filterParams.Title.ToLower()));
				}

				if (messageStatus != null)
					result = result?.Where(x => x.Status == messageStatus);

				MessageUserAdminFilteredPaging model = new();
				model.GetBasePagination(result, filterParams.PageId, filterParams.Take);
				model.FilterParams = filterParams;
				model.Messages = new();

				if (result != null)
					model.Messages = await result.Skip(model.Skip).Take(model.Take).OrderByDescending(x => x.CreateDate)
						.Select(x => new MessageUserAdminQueryModel
						{
							Id = x.Id,
							CreateDate = x.CreateDate.ConvertToPersianDate(),
							UserId = x.UserId,
							Status = x.Status,
							UserName = x.FullName,
							Subject = x.Subject,
							PhoneNumber = x.PhoneNumber,
							Email = x.Email,
							Message = x.Message,
							AnswerSms = x.AnswerSms,
							AnswerEmail = x.AnswerEmail
						}).ToListAsync();

				return model;

			}
			catch (Exception e)
			{
				return new();
			}

		}

		public async Task<MessageUserAdminQueryModel?> GetMessageUserDetailForAdmin(long id)
		{
			try
			{
				var messageUser = _messageUserRepository.GetById(id);

				if (messageUser == null)
					throw new NullReferenceException();

				return new()
				{
					Id = messageUser.Id,
					CreateDate = messageUser.CreateDate.ConvertToPersianDate(),
					UserId = messageUser.UserId,
					Status = messageUser.Status,
					UserName = messageUser.FullName,
					Subject = messageUser.Subject,
					PhoneNumber = messageUser.PhoneNumber,
					Email = messageUser.Email,
					Message = messageUser.Message,
					AnswerSms = messageUser.AnswerSms,
					AnswerEmail = messageUser.AnswerEmail
				};
			}
			catch (Exception e)
			{
				return null;
			}
		}
	}
}
