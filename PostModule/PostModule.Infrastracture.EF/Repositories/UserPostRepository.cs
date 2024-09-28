using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories;

internal class UserPostRepository : Repository<int, UserPost>, IUserPostRepository
{
    private readonly PostContext _context;
    public UserPostRepository(PostContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserPost> GetByApiCode(string apiCode)
    {
        return await _context.UserPosts.SingleOrDefaultAsync(p => p.ApiCode == apiCode);
    }

    public async Task<UserPost> GetForUser(long userId)
    {
        UserPost userPost = await _context.UserPosts.SingleOrDefaultAsync(p => p.UserId == userId);
        if(userPost == null)
        {
            userPost = new UserPost(userId, 50, Guid.NewGuid().ToString());
            await _context.UserPosts.AddAsync(userPost);
            await _context.SaveChangesAsync();
        }
        return userPost;
    }
}