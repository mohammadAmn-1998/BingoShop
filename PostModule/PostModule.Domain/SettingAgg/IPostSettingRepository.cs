using PostModule.Application.Contract.PostSettingApplication.Command;
using Shared.Domain;
using Shared.Domain.SeedWorks.Base;

namespace PostModule.Domain.SettingAgg
{
    public interface IPostSettingRepository : IRepository<int,PostSetting>
    {
        UbsertPostSetting GetForUbsert();
        PostSetting GetSingle();
    }
}
