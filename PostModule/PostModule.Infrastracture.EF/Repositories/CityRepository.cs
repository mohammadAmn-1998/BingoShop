using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using PostModule.Domain.Services;
using Shared.Domain.Enums;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories
{
    internal class CityRepository : Repository<int ,City>, ICityRepository
    {
        
        public CityRepository(PostContext context) : base(context)
        {
            
        }

		public async Task<bool> ChangeStatus(int id, CityStatus status)
		{
			var city = GetBy(x => x.Id == id);
            List<City> cities = new();
            if(status == CityStatus.تهران)
            {
                 cities = GetAll().Where(c => c.Status == CityStatus.تهران).ToList();
            }
            else if(status == CityStatus.مرکز_استان)
            {
                cities = GetAll().Where(c => c.Status == CityStatus.مرکز_استان && c.StateId == city.StateId).ToList();
			}
            city.ChangeStatus(status);
            
            if(cities.Count() > 0)
			foreach (var item in cities)
			{
				item.ChangeStatus(CityStatus.شهرستان_معمولی);
			}
            return Save();
		}

		public List<CityViewModel> GetAllForState(int stateId)
        {
            return GetAll(c => c.StateId == stateId).Select(c => new CityViewModel
            {
                CreateDate=c.CreateDate.ToString(),
                Id=c.Id,
                Status=c.Status,
                Title=c.Title
            }).ToList();
        }

        public EditCityModel GetCityForEdit(int id)
        {
            var city = GetBy(x=> x.Id == id);
            return new()
            {
                Id=city.Id,
                Title = city.Title,
                StateId = city.StateId, 
            };
        }
    }
}
