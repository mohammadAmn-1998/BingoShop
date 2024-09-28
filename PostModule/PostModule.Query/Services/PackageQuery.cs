using PostModule.Application.Contract.UserPostApplication.Query;
using PostModule.Domain.UserPostAgg;
using Shared.Application;
using Shared.Application.Utility;

namespace PostModule.Query.Services
{
    public class PackageQuery : IPackageQuery
    {
        private readonly IPackageRepository _packageRepository;

        public PackageQuery(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }

        public List<PackageAdminQueryModel> GetAll()
        {
            IQueryable<Package> model = _packageRepository.GetAsQueryable();
            return model.Select(p => new PackageAdminQueryModel(p.Id, p.Title, p.Count, p.Price,
                p.CreateDate.ConvertToPersianDate(), p.UpdateDate.ConvertToPersianDate(), p.Active, Directories.PackageImageDirectory100 + p.ImageName))
            .ToList();
        }
    }
}