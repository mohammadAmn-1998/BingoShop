using System.Linq.Expressions;
using Blogs1.Application.Contract.BlogService.Command;

namespace Blogs1.Domain.BlogAgg.IRepositories;

public interface IBlogRepository
{
	Task<EditBlog?> GetForEdit(long  blogId);

	Task<Blog?> GetById(long id);

	Task<bool> Exists(Expression<Func<Blog, bool>> expression);

	Task<bool> Create(CreateBlog command);
	Task<bool> Edit(EditBlog command);

	Task<bool> ActivationChange(long blogId);

	Task<bool> SpecialationChange(long blogId);
	Task<bool> IncreaseVisits(string slug);
}