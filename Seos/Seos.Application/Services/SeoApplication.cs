
using Seos.Application.Contract;
using Seos.Domain;
using Shared.Domain.Enums;

namespace Seos.Application.Services
{
    internal class SeoApplication : ISeoApplication
    {
        private readonly ISeoRepository _seoRepository;
        public SeoApplication(ISeoRepository seoRepository)
        {
            _seoRepository = seoRepository;
        }

        public CreateSeo GetSeoForEdit(long ownerId, WhereSeo where)
        {
            return _seoRepository.GetSeoForUbsert(ownerId, where);
        }

        public async Task<bool> UbsertSeo(CreateSeo command)
        {
	        return await _seoRepository.Create(command);
        }


    }
}
