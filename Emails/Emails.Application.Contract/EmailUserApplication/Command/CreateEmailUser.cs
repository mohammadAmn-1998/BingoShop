namespace Emails.Application.Contract.EmailUserApplication.Command
{
    public class CreateEmailUser
    {
        public long UserId { get; set; }
        public string Email { get; set; }
    }
}
