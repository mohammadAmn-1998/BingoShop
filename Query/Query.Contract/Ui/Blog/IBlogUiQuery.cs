﻿using Query.Contract.Ui.Seo;
using Shared.Application.Models;

namespace Query.Contract.Ui.Blog;

public interface IBlogUiQuery
{
	Task<BlogUIPaging> GetBlogsForUI(FilterParams filterParams, string categorySlug = "");
	Task<SingleBlogUIQueryModel> GetSingleBlog(string slug);
}

public class SingleBlogUIQueryModel
{

	public long Id { get; set; }

	public string Title { get; set; }

	public string Summary { get; set; }

	public string Content { get; set; }

	public string CreateDate { get; set; }

	public string ImageName { get; set; }

	public string ImageAlt { get; set; }

	public long Likes { get; set; }

	public long CategoryId { get; set; }

	public long SubCategoryId { get; set; }


	public string Author { get; set; }

	public string AuthorAvatar { get; set; }

	public string? AuthorAvatarImageAlt { get; set; }

	public long TotalComments { get; set; }

	public long TotalVisits { get; set; }


	public CategoryUIQueryModel Category { get; set; }

	public CategoryUIQueryModel SubCategory { get; set; }

	public SeoUIQueryModel Seo { get; set; }

	public List<BreadCrumbUiQueryModel> BreadCrumbs { get; set; }



}