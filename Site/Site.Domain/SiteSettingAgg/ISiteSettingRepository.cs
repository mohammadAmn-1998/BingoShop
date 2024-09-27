using Site.Application.Contract.SiteSettingApplication.Command;

namespace Site.Domain.SiteSettingAgg
{
    public interface ISiteSettingRepository
    {
        Task<UbsertSiteSetting> GetForUbsert();
        Task<SiteSetting?> GetSingle();

		Task<bool> Edit(SiteSetting siteSetting);

    }
}
