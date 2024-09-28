using Shared.Domain;
using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;


namespace PostModule.Domain.Services
{
    public interface ICityRepository : IRepository<int,City>
    {
		Task<bool> ChangeStatus(int id, CityStatus status);
		List<CityViewModel> GetAllForState(int stateId);
        EditCityModel GetCityForEdit(int id);
    }
}
