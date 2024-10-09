using Shared.Application.Models;

namespace Blogs1.Application.Contract.BlogService.Query;

public interface IBlogQuery
{
	
	Task<FilteredBlogQueryModel?> GetFilteredPosts(FilterParams  filterParams);

	List<PopularBlogQueryModel> GetPopularBlogsForUI();
	List<LastBlogQueryModel> GetLastBlogsForUI();
	List<SpecialBlogForUIQueryModel> GetSpecialBlogsForUI();


}