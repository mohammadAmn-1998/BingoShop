using Shared.Infrastructure;
using PostModule.Domain.UserPostAgg;
using PostModule.Application.Contract.UserPostApplication.Command;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PackageRepository : Repository<int, Package>, IPackageRepository
{
    private readonly PostContext _context;
    public PackageRepository(PostContext context) : base(context)
    {
        _context = context;
    }

    public async Task<CreatePostOrder> GetCreatePostModelAsync(long userId, int packageId)
    {
        var package = await _context.Packages.FindAsync(packageId);
        return new CreatePostOrder(userId,package.Id,package.Price);
    }

    public EditPackage GetForEdit(int id)
    {
        return _context.Packages.Select(p => new EditPackage
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Count = p.Count,
            Price = p.Price,
            ImageAlt = p.ImageAlt,
            ImageFile = null,
            ImageName = p.ImageName
        }).SingleOrDefault(p => p.Id == id);
    }
}
