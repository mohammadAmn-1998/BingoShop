using System.Linq.Expressions;
using Blogs1.Application.Contract.BlogCategoryService.Command;
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogService.Command;

public interface IBlogService
{

	Task<OperationResult> Create(CreateBlog command);

	Task<EditBlog?> GetForEdit(long id);

	Task<OperationResult> Edit(EditBlog  command);

	Task<bool> ActivationChange(long blogId);

	Task<bool> SpecialationChange(long blogId);
	Task<bool> IncreaseVisits(string slug);
}