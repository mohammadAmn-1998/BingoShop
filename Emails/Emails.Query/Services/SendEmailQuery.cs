using Emails.Application.Contract.SendEmailApplication.Query;
using Emails.Domain.SendEmailAgg;
using Shared.Application.Utility;

namespace Emails.Query.Services
{
	internal class SendEmailQuery : ISendEmailQuery
	{
		private readonly ISendEmailRepository _sendEmailRepository;
        public SendEmailQuery(ISendEmailRepository sendEmailRepository)
        {
            _sendEmailRepository = sendEmailRepository;
        }
        public List<SendEmailQueryModel> GetEmailSendsFoeAdmin()
		{
			return _sendEmailRepository.GetAsQueryable()
				.Select(x => new SendEmailQueryModel()
				{
					CreationDate = x.CreateDate.ConvertToPersianDate(),
					Id = x.Id,
					Title = x.Title
				}).ToList();
		}

		public SendEmailDetailQueryModel GetSendEmailDetailForAdmin(int id)
		{
			var email = _sendEmailRepository.GetById(id);
			return new()
			{
				CreationDate = email.CreateDate.ConvertToPersianDate(),
				Id = email.Id,
				Text = email.Text,
				Title = email.Title
			};
		}
	}
}
