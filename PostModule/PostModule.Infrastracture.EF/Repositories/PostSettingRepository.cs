using Shared.Infrastructure;
using PostModule.Domain.SettingAgg;
using PostModule.Application.Contract.PostSettingApplication.Command;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories;

internal class PostSettingRepository : Repository<int, PostSetting>, IPostSettingRepository
{
    private readonly PostContext _context;
    public PostSettingRepository(PostContext context) : base(context)
    {
        _context = context;
    }

    public UbsertPostSetting GetForUbsert()
    {
        var s = GetSingle();
        return new()
        {
            PackageDescription = s.PackageDescription,
            PackageTitle = s.PackageTitle,
            ApiDescription = s.ApiDescription
        };
    }

    public PostSetting GetSingle()
    {
        var setting = _context.PostSettings.SingleOrDefault();
        if(setting == null)
        {
            setting = new("", "","");
            Insert(setting);
        }
        return setting;
    }
}