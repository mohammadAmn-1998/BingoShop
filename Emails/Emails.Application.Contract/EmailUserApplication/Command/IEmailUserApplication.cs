using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Emails.Application.Contract.EmailUserApplication.Command
{
    public interface IEmailUserApplication
    {
        OperationResult Create(CreateEmailUser command);
        bool ActivationChange(int id);
    }
}
