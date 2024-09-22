
using Shared.Application.Utility;

namespace Blogs1.Application.Contract.BlogCategoryService.Command;

public interface IBlogCategoryService
{

	Task<OperationResult> CreateCategory(CreateBlogCategory command);
	Task<OperationResult> EditCategory(EditBlogCategory command);
	Task<OperationResult> DeleteCategory(long id);

	Task<EditBlogCategory?> GetCategoryForEdit(long id);

	Task<bool> ChangeActivation(long categoryId);
}