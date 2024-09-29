namespace Query.Contract.Admin.EmailUser;

public class EmailUserAdminQueryModel
{
	public long Id { get; set; }

	public long UserId { get; set; }

	public string Email { get; set; }

	public bool IsActive { get; set; }

	public string CreateDate { get; set; }

}