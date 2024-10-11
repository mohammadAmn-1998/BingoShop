using Query.Contract.Ui.Seo;
using Shared.Application.Models;
using Shared.Application.Utility;

namespace Query.Contract.Ui.Blog;

public class BlogUIPaging : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<BlogUiQueryModel> Blogs { get; set; }

	public SeoUIQueryModel Seo { get; set; }

	public List<BreadCrumbUiQueryModel> BreadCrumbs { get; set; }

	public string Slug { get; set; }

	public string PageTitle { get; set; }

}