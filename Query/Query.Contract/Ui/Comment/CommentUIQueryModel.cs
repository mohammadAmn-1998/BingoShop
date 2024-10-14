namespace Query.Contract.Ui.Comment;

public class CommentUIQueryModel
{


	public long Id { get; set; }

	public string FullName { get; set; }

	public string Text { get; set; }

	public string? Avatar { get; set; }

	public string CreationDate { get; set; }

	public long? ParentId { get; set; }

	public List<CommentUIQueryModel> Childs { get; set; }

}