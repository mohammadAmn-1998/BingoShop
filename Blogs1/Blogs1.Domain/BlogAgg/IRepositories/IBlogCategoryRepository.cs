using System.Linq.Expressions;
using Blogs1.Application.Contract.BlogCategoryService.Command;


namespace Blogs1.Domain.BlogAgg.IRepositories;

public interface IBlogCategoryRepository
{
	Task<BlogCategory?> GetBy(long categoryId);

	Task<bool> Exists(Expression<Func<BlogCategory, bool>> expression);

	Task<bool> Create(CreateBlogCategory command);

	Task<bool> ChangeActivation(long categoryId);

	Task<bool> Update(EditBlogCategory command);

	Task<BlogCategory?> GetById(long categoryId);
}