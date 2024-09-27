

using Shared.Domain.Enums;

namespace Site.Application.Contract.BanerApplication.Query
{
    public interface IBanerQuery
    {
        List<BanerForAdminQueryModel> GetAllForAdmin();
        List<BanerForUiQueryModel> GetForUi(int count, BanerState state);   
    }
}
