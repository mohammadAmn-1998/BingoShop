using Microsoft.EntityFrameworkCore;
using Site.Application.Contract.SiteSettingApplication.Command;
using Site.Domain.SiteSettingAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Site.Infrastructure.Services
{
    internal class SiteSettingRepository : BaseRepository,  ISiteSettingRepository
    {
       

        public SiteSettingRepository(SiteContext context) : base(context) 
        {
            
        }

        public async Task<UbsertSiteSetting> GetForUbsert()
        {
            var site = await GetSingle();
            return new()
            {
                AboutDescription = site.AboutDescription,
                AboutTitle = site.AboutTitle,
                Address = site.Address,
                Android = site.Android,
                LogoAlt = site.LogoAlt,
                WhatsApp = site.WhatsApp,
                ContactDescription = site.ContactDescription,
                Email1 = site.Email1,
                Email2 = site.Email2,
                Enamad = site.Enamad,
                FavIcon = site.FavIcon,
                FavIconFile = null,
                FooterDescription = site.FooterDescription,
                FooterTitle = site.FooterTitle,
                Instagram = site.Instagram,
                IOS = site.IOS,
                LogoFile = null,
                LogoName = site.LogoName,
                Phone1 = site.Phone1,
                Phone2 = site.Phone2,
                SamanDehi = site.SamanDehi,
                SeoBox = site.SeoBox,
                Telegram = site.Telegram,
                Youtube = site.Youtube
            };
        }

        public async Task<SiteSetting?> GetSingle()
        {
            SiteSetting? site = Table<SiteSetting>().SingleOrDefault();
            if(site == null)
            {
                site = new();
                Insert(site);
               await Save();
            }
            return site;    
        }

        public async Task<bool> Edit(SiteSetting siteSetting)
        {
	        try
	        {
				Update(siteSetting);
				return await Save() > 0;
	        }
	        catch (Exception e)
	        {
		        return false;
	        }
        }
    }
}
