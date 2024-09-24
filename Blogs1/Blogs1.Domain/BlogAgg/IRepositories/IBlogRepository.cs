using System.Linq.Expressions;
using Blogs1.Application.Contract.BlogService.Command;

namespace Blogs1.Domain.BlogAgg.IRepositories;

public interface IBlogRepository
{

	Task<bool> Exists(Expression<Func<Blog, bool>> expression);

	Task<bool> Create(CreateBlog command);

	Task<bool> ActivationChange(long blogId);

	Task<bool> SpecialationChange(long blogId);

}