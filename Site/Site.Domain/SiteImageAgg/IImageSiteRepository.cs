using Shared.Domain;
using Site.Application.Contract.ImageSiteApplication.Command;

namespace Site.Domain.SiteImageAgg
{
	public interface IImageSiteRepository
	{

		Task<bool> Create(CreateImageSite command);

		Task<SiteImage?> GetById(int id);

		Task<bool> Delete(int id);
	}
}
