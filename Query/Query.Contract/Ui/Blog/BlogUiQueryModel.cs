namespace Query.Contract.Ui.Blog;

public class BlogUiQueryModel
{

	public string Title { get; set; }

	public string Summary { get; set; }

	public string ImageName { get; set; }

	public string  ImageAlt { get; set; }

	public string Slug { get; set; }

	public long CategoryId { get; set; }

	public string Author { get; set; }

	public string CreateDate { get; set; }

	public CategoryUIQueryModel Category { get; set; }

}