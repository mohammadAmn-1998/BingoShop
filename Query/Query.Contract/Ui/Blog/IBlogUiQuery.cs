using Shared.Application.Models;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace Query.Contract.Ui.Blog;

public interface IBlogUiQuery
{
	Task<BlogUIPaging> GetBlogsForUI(FilterParams filterParams, string categorySlug = "");
}



public class BlogUIPaging : BasePagination
{

	public FilterParams FilterParams { get; set; }

	public List<BlogUIQueryModel> Blogs { get; set; }

	public SeoUIQueryModel Seo { get; set; }

	public List<BreadCrumbUiQueryModel> BreadCrumbs { get; set; }

	public string Slug { get; set; }

	public string PageTitle { get; set; }

}

public class BreadCrumbUiQueryModel
{
	public int Number { get; set; }

	public string Title { get; set; }

	public string Url { get; set; }

}

public class SeoUIQueryModel
{

	public WhereSeo Where { get; private set; }
	public long OwnerId { get; private set; }
	public string MetaTitle { get; private set; }
	public string? MetaDescription { get; private set; }
	public string? MetaKeyWords { get; private set; }
	public bool IndexPage { get; private set; }
	public string? Canonical { get; private set; }
	public string? Schema { get; private set; }

	public SeoUIQueryModel()
	{
		
	}

	public SeoUIQueryModel(WhereSeo where, long ownerId, string metaTitle, string? metaDescription, string? metaKeyWords, bool indexPage, string? canonical, string? schema)
	{
		Where = where;
		OwnerId = ownerId;
		MetaTitle = metaTitle;
		MetaDescription = metaDescription;
		MetaKeyWords = metaKeyWords;
		IndexPage = indexPage;
		Canonical = canonical;
		Schema = schema;
	}
}

public class BlogUIQueryModel
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

public class CategoryUIQueryModel
{

	public string Title { get; set; }

	public string Slug { get; set; }

}