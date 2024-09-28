using PostModule.Application.Contract.CityApplication;
using PostModule.Domain.CityEntity;
using PostModule.Domain.Services;
using Shared.Application.Utility;
using Shared.Domain.Enums;

namespace PostModule.Application.Services
{
    internal class CityApplication : ICityApplication
    {
        private readonly ICityRepository _cityRepository;
        public CityApplication(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

		public async Task<bool> ChangeStatus(int id, CityStatus status)=>
         await   _cityRepository.ChangeStatus(id, status);   

		public OperationResult Create(CreateCityModel command)
        {
            if(_cityRepository.IsExists(c=>c.Title == command.Title && c.StateId == command.StateId))
                return new(Status.BadRequest,ErrorMessages.DuplicateError,nameof(command.Title));
            City city = new(command.StateId, command.Title, CityStatus.شهرستان_معمولی);
            if (_cityRepository.Insert(city))
            {
				return new(Status.Success);
			}
            return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.Title));
        }

        public OperationResult Edit(EditCityModel command)
        {
			if (_cityRepository.IsExists(c => c.Title == command.Title && c.StateId == command.StateId && c.Id != command.Id))
				return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));
			City city = _cityRepository.GetBy(x=> x.Id == command.Id);
            city.Edit(command.Title, city.Status);
			if (_cityRepository.Save()) return new(Status.Success);
            return new(Status.BadRequest, ErrorMessages.InternalServerError, nameof(command.Title));
		}

        public bool ExistTitleForCreate(string title , int stateid) =>
            _cityRepository.IsExists(c => c.Title == title && c.StateId == stateid);

        public bool ExistTitleForEdit(string title, int id, int stateid) =>
            _cityRepository.IsExists(c => c.Title == title && c.StateId == stateid && c.Id != id);

        public List<CityViewModel> GetAllForState(int stateId) => 
            _cityRepository.GetAllForState(stateId);

        public EditCityModel GetCityForEdit(int id) =>
            _cityRepository.GetCityForEdit(id);
    }
}
