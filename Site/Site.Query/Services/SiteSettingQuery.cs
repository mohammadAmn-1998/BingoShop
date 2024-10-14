using Shared.Application;
using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Domain.MenuAgg;
using Site.Domain.SiteSettingAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Query.Contract.Ui.Blog;
using Query.Contract.Ui.Seo;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Site.Infrastructure;

namespace Site.Query.Services;

internal class SiteSettingQuery :BaseRepository, ISiteSettingQuery
{

	private readonly ISeoUIQuery _seoUiQuery;
	public SiteSettingQuery(SiteContext context, ISeoUIQuery seoUiQuery) : base(context)
	{
		_seoUiQuery = seoUiQuery;
	}

    public ContactFooterUiQueryModel GetContactDataForFooter()
    {
        var site = Table<SiteSetting>().Single();
        return new ContactFooterUiQueryModel(site.Address, site.Phone1, site.Email1, site.Android, site.IOS);
    }

    public async Task<ContactUIQueryModel> GetContactDataForContactUs()
    {
	    try
	    {

		    ContactUIQueryModel model = new();
		    model.Seo =await _seoUiQuery.GetSeoForUI(0, WhereSeo.Contact,"تماس با ما");
		    model.BreadCrumbs = new List<BreadCrumbUiQueryModel>()
		    {
			    new()
			    {
				    Number = 1, Title = "خانه", Url = "/"
			    },
			    new()
			    {
				    Number = 1, Title = "تماس با ما", Url = ""
			    }
		    };
		    var site = Table<SiteSetting>().Single();
		   
			model.Address =site.Address;
			model.Phone1 =site.Phone1;
			model.Phone2 =site.Phone2;
			model.Email1 =site.Email1;
			model.Email2 = site.Email2;

			return model;
	    }
	    catch (Exception e)
	    {
		    return new ContactUIQueryModel()
		    {
			    BreadCrumbs = new List<BreadCrumbUiQueryModel>()
			    {
				    new()
				    {
					    Number = 1, Title = "خانه", Url = "/"
				    },
				    new()
				    {
					    Number = 1, Title = "تماس با ما", Url = ""
				    }
			    },
				Seo = new()
};
	    }
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
