using Shared.Application;
using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Domain.MenuAgg;
using Site.Domain.SiteSettingAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;
using Site.Infrastructure;

namespace Site.Query.Services;

internal class SiteSettingQuery :BaseRepository, ISiteSettingQuery
{
	public SiteSettingQuery(SiteContext context) : base(context)
	{
		
	}

    public ContactFooterUiQueryModel GetContactDataForFooter()
    {
        var site = Table<SiteSetting>().Single();
        return new ContactFooterUiQueryModel(site.Address, site.Phone1, site.Email1, site.Android, site.IOS);
    }

    public FavIconForUiQueryModel GetFavIconForUi()
    {
        var site =Table<SiteSetting>().Single();
        return new FavIconForUiQueryModel(site.FavIcon);
    }

    public FooterUiQueryModel GetFooter()
    {
        var site = Table<SiteSetting>().Single();
        return new FooterUiQueryModel(site.Enamad, site.SamanDehi, site.FooterTitle, site.FooterDescription);
    }

    public LogoForUiQueryModel GetLogoForUi()
    {
        var site = Table<SiteSetting>().Single();
        return new LogoForUiQueryModel(site.LogoName, site.LogoAlt);
    }

    public SocialForUiQueryModel GetSocialForUi()
    {
        var site = Table<SiteSetting>().Single();
        return new SocialForUiQueryModel(site.Instagram, site.WhatsApp, site.Telegram, site.Youtube);
    }
}
