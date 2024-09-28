using Emails.Application.Contract.MessageUserApplication.Command;
using Emails.Domain.MessageUserAgg;
using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Emails.Application.Services
{
    internal class MessageUserApplication : IMessageUserApplication
    {
        private readonly IMessageUserRepository _messageUserRepository;
        public MessageUserApplication(IMessageUserRepository messageUserRepository)
        {
            _messageUserRepository = messageUserRepository;
        }

        public bool AnswerByCall(long id)
        {
            var messageUser = _messageUserRepository.GetById(id);
            messageUser.AnswerByCall();
            return _messageUserRepository.Save();
        }

        public OperationResult AnsweredByEmail(long id, string mailMessage)
        {
			try
			{
				var messageUser = _messageUserRepository.GetById(id);
				messageUser.AnswerEmailSend(mailMessage);
				_messageUserRepository.Save();
				//
				// send sms
				//
				return new(Status.Success);
			}
			catch (Exception)
			{
				return new(Status.InternalServerError,ErrorMessages.InternalServerError);
			}
		}

        public OperationResult AnsweredBySMS(long id, string message)
        {

            try
            {
				var messageUser = _messageUserRepository.GetById(id);
				messageUser.AnswerSmsSend(message);
				_messageUserRepository.Save();
				//
				// send sms
				//
				return new(Status.Success);
			}
            catch (Exception)
            {
                return new(Status.InternalServerError,ErrorMessages.InternalServerError);
            }
        }

        public OperationResult Create(CreateMessageUser command)
        {
            MessageUser messageUser = new(command.UserId, command.FullName, command.Subject, 
                command.PhoneNumber, command.Email, command.Message);
            if (_messageUserRepository.Insert(messageUser))
                return new(Status.Success);
            return new(Status.InternalServerError,ErrorMessages.InternalServerError);  
        }
    }
}
