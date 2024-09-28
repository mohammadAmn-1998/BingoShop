using Microsoft.Extensions.DependencyInjection;
using PostModule.Application.Contract.PostQuery;
using PostModule.Application.Contract.StateQuery;
using PostModule.Application.Contract.UserPostApplication.Query;
using PostModule.Application.Services;
using PostModule.Domain.Services;
using PostModule.Infrastracture.EF;
using PostModule.Infrastracture.EF.Repositories;
using PostModule.Query.Services;

namespace PostModule.Query
{
    public class PostQueryBootstrapper
    {
        public static void Config(IServiceCollection services )
        {
           

            services.AddTransient<IStateQuery, StateQuery>();
            services.AddTransient<IPostQuery, PostQuery>();
            services.AddTransient<IPackageQuery, PackageQuery>();
        }
    }
}