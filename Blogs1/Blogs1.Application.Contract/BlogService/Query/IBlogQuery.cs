using Shared.Application.Models;
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogService.Query;

public interface IBlogQuery
{
	
	Task<FilteredBlogQueryModel?> GetFilteredPosts(FilterParams  filterParams);
	


}

public class FilteredBlogQueryModel : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<BlogQueryModel> Blogs { get; set; }

}

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

	public DateTime CreateDate { get; set; } = DateTime.Now;

	public DateTime? UpdateDate { get; set; }

	public bool Active { get; set; }

	

}