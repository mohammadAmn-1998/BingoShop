using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace PostModule.Application.Contract.CityApplication
{
    public interface ICityApplication
    {
        OperationResult Create(CreateCityModel command);
        OperationResult Edit(EditCityModel command);
        bool ExistTitleForCreate(string title, int stateid);
        bool ExistTitleForEdit(string title , int id, int stateid);
        EditCityModel GetCityForEdit(int id);
        List<CityViewModel> GetAllForState(int stateId);
        Task<bool> ChangeStatus(int id, CityStatus status);
    }
}
