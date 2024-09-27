
using Shared.Domain.Enums;

namespace Seos.Application.Contract
{
    public interface ISeoApplication
    {
        Task<bool> UbsertSeo(CreateSeo command);
        CreateSeo GetSeoForEdit(long ownerId, WhereSeo where);
    }
}
