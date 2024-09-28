using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using Microsoft.EntityFrameworkCore;
using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Application;
using Shared.Application.Utility;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PostOrderRepository : Repository<int, PostOrder>, IPOstOrderRepository
{
    private readonly PostContext _context;
    public PostOrderRepository(PostContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PostOrderUserPanelModel> GetPostOrderNotPaymentForUser(long userId)
    {
        var postOrder = await GetPostOrderNotPaymentForUserAsync(userId);
        if (postOrder == null) return null;
        var package = await _context.Packages.FindAsync(postOrder.PackageId);
        if(package.Price != postOrder.Price)
        {
            postOrder.Edit(package.Id, package.Price);
            await _context.SaveChangesAsync();
        }
        return new PostOrderUserPanelModel(postOrder.Id, postOrder.PackageId, package.Title, postOrder.Price,
            $"{Directories.PackageImageDirectory400}{package.ImageName}", package.ImageAlt, package.Count,package.Description);
    }

    public async Task<PostOrder> GetPostOrderNotPaymentForUserAsync(long userId) =>
        await _context.PostOrders.SingleOrDefaultAsync(p => p.UserId == userId 
        && p.Status == Shared.Domain.Enums.PostOrderStatus.پرداخت_نشده);
}
