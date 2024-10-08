using Emails.Application.Contract.EmailUserApplication.Command;
using Emails.Domain.EmailUserAgg;
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
    internal class EmailUserApplication : IEmailUserApplication
    {
        private readonly IEmailUserRepository _emailUserRepository;

        public EmailUserApplication(IEmailUserRepository emailUserRepository)
        {
            _emailUserRepository = emailUserRepository;
        }

        public bool ActivationChange(long id)
        {
	        try
	        {
		        var email = _emailUserRepository.GetById(id);
		        email.ActivationChange();
		        return _emailUserRepository.Save();
}
	        catch (Exception e)
	        {
		        return false;
	        }
           
        }

        public OperationResult Create(CreateEmailUser command)
        {
            if (_emailUserRepository.IsExists(e => e.Email.Trim().ToLower() == command.Email.Trim().ToLower()))
                return new(Status.BadRequest, ErrorMessages.DuplicateEmailAddressError);
            EmailUser emailUser = new(command.Email.Trim().ToLower(), command.UserId);
            if (_emailUserRepository.Insert(emailUser))
                return new(Status.Success);
            return new(Status.InternalServerError, ErrorMessages.InternalServerError);
        }
    }
}
