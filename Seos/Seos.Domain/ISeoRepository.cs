
using Seos.Application.Contract;
using Shared.Domain.Enums;

namespace Seos.Domain
{
    public interface ISeoRepository 
    {
        CreateSeo GetSeoForUbsert(long ownerId, WhereSeo where);
        Seo? GetSeo(long ownerId, WhereSeo where);
        Task<Seo> GetSeoForUi(long ownerId, WhereSeo where, string title);

        Task<bool> Create(CreateSeo command);
    }
}
