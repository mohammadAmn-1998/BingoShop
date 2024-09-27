namespace Blogs1.Application.Contract.BlogService.Query;

public class BlogQueryModel
{
	public long Id { get; set; }

	public string Title { get;  set; }

	public string ImageName { get;  set; }

	public string ImageAlt { get;  set; }

	public long UserId { get;  set; }

	public string Author { get;  set; }

	public long CategoryId { get;  set; }

	public string CategoryTitle { get; set; }

	public string? SubCategoryTitle { get; set; }

	public long SubCategoryId { get;  set; }

	public string Slug { get;  set; }

	public string Summary { get;  set; }

	public string Content { get;  set; }

	public long TotalVisits { get;  set; }

	public bool IsSpecial { get;  set; }

	public long Likes { get;  set; }

	public long Dislikes { get;  set; }

	public string CreateDate { get; set; } 

	public string? UpdateDate { get; set; }

	public bool Active { get; set; }

	

}