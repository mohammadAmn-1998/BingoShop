using Shared.Application.Utility;

namespace Emails.Application.Contract.MessageUserApplication.Command
{
    public interface IMessageUserApplication
    {
        OperationResult Create(CreateMessageUser command);
        OperationResult AnsweredBySMS(long id,string message);
        OperationResult AnsweredByEmail(long id,string mailMessage);
        bool AnswerByCall(long id);
    }
}
