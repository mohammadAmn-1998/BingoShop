
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.ImageSiteApplication.Command;
using Site.Domain.SiteImageAgg;

namespace Site.Infrastructure.Services;

internal class ImageSiteRepository : BaseRepository, IImageSiteRepository
{
	

	public ImageSiteRepository(SiteContext context) : base(context)
	{
		
	}

	public async Task<bool> Create(CreateImageSite command)
	{
		try
		{
			SiteImage imageSite = new(command.ImageName!, command.Title);
			Insert(imageSite);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}
}