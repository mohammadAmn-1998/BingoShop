namespace Emails.Application.Contract.SendEmailApplication.Query
{
	public class SendEmailDetailQueryModel
	{
		public long Id { get; set; }
		public string Title { get; set; }
		public string Text { get; set; }
		public string CreationDate { get; set; }
	}
}
