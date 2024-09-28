using Emails.Domain.SendEmailAgg;
using Emails.Application.Contract.SendEmailApplication.Command;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Emails.Application.Services
{
    internal class SendEmailApplication : ISendEmailApplication
    {
        private readonly ISendEmailRepository _sendEmailRepository;
        public SendEmailApplication(ISendEmailRepository sendEmailRepository)
        {
            _sendEmailRepository = sendEmailRepository; 
        }
        public OperationResult Create(CreateSendEmail commmand)
        {
            SendEmail email = new(commmand.Title, commmand.Text);
            if (_sendEmailRepository.Insert(email)) return new(Status.Success);
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(commmand.Title));
        }
    }
}
