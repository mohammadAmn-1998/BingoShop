using Microsoft.Extensions.DependencyInjection;
using Site.Application.Contract.BanerApplication.Query;
using Site.Application.Contract.ImageSiteApplication.Query;
using Site.Application.Contract.MenuApplication.Query;
using Site.Application.Contract.SitePageApplication.Query;
using Site.Application.Contract.SiteServiceApplication.Query;
using Site.Application.Contract.SiteSettingApplication.Query;
using Site.Application.Contract.SliderApplication.Query;
using Site.Infrastructure;
using Site.Query.Services;


namespace Site.Query
{
    public static class SiteQueryBootstrapper
    {
        public static void Config(IServiceCollection services)
        {
            

            services.AddTransient<IBanerQuery,BanerQuery>();    
            services.AddTransient<IMenuQuery,MenuQuery>();    
            services.AddTransient<ISliderQuery,SliderQuery>();    
            services.AddTransient<ISiteSettingQuery, SiteSettingQuery>();    
            services.AddTransient<ISiteServiceQuery,SiteServiceQuery>();
            services.AddTransient<ISitePageQuery,SitePageQuery>();
            services.AddTransient<IImageSiteQuery, ImageSiteQuery>();
        }
    }
}
