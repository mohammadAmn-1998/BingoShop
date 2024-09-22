namespace Blogs1.Application.Contract.BlogCategoryService.Query;

public class BlogCategoryQueryModel
{

	public long Id { get; set; }

	public DateTime CreateDate { get; set; }

	public string Title { get;  set; }

	public string ImageName { get;  set; }

	public string ImageAlt { get;  set; }

	public long ParentId { get;  set; }

	public string Slug { get;  set; }

	public bool IsActive { get; set; }

	public List<BlogCategoryQueryModel>? SubCategories { get; set; }

}