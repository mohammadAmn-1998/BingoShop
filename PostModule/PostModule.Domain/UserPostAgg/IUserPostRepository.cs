using Shared.Domain;
using Shared.Domain.SeedWorks.Base;

namespace PostModule.Domain.UserPostAgg
{
    public interface IUserPostRepository : IRepository<int,UserPost>
    {
        Task<UserPost> GetForUser(long userId);
        Task<UserPost> GetByApiCode(string apiCode);
    }
}
