using Shared.Application.Models;
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogService.Query;

public interface IBlogQuery
{
	
	Task<FilteredBlogQueryModel?> GetFilteredPosts(FilterParams  filterParams);

	List<PopularBlogQueryModel> GetPopularBlogsForUI();
	List<LastBlogQueryModel> GetLastBlogsForUI();


}

public class FilteredBlogQueryModel : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<BlogQueryModel> Blogs { get; set; }

}
