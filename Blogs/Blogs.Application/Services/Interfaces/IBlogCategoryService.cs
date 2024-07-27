using Blogs.Application.Dtos.BlogCategories;
using Shared.Application.Utility;
using System.Linq.Expressions;
using Blogs.Domain.Agg.CategoryAgg;

namespace Blogs.Application.Services.Interfaces;

public interface IBlogCategoryService
{

	OperationResult Create(CreateBlogCategoryDto dto);

	OperationResult Edit(EditBlogCategoryDto dto);

	EditBlogCategoryDto GetForEdit(int categoryId);

	bool Exists(Expression<Func<BlogCategory, bool>> expression);

	void ActivationChange(int categoryId);
	

}