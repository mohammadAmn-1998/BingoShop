using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Domain;
using Shared.Domain.SeedWorks.Base;

namespace PostModule.Domain.UserPostAgg
{
    public interface IPackageRepository : IRepository<int,Package>
    {
        Task<CreatePostOrder> GetCreatePostModelAsync(long userId, int packageId);
        EditPackage GetForEdit(int id);
    }
}
