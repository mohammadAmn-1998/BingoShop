
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Domain.SiteServiceAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Domain.SeedWorks.Base;

namespace Site.Infrastructure.Services;

internal class SiteServiceRepository : BaseRepository , ISiteServiceRepository
{
   

    public SiteServiceRepository(SiteContext context) : base(context)
    {
        
    }

    public EditSiteService? GetForEdit(long id) =>
       Table<SiteService>().Select(s => new EditSiteService
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName,
            Title = s.Title
        }).SingleOrDefault(s => s.Id == id);

    public async Task<bool> ChangeActivation(long id)
    {
	    try
	    {
		    var sitePage = await GetById<SiteService>(id);
		    if (sitePage == null)
			    throw new NullReferenceException();

		    sitePage.ActivationChange();
		    Update(sitePage);
		    return await Save() > 0;
	    }
	    catch (Exception re)
	    {
		    return false;
	    }

}
	    
    

    public async Task<bool> Create(SiteService service)
    {
		try
		{
			Insert(service);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
}

    public async Task<bool> Edit(SiteService service)
    {
		try
		{
			Update(service);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
}

    public async Task<SiteService?> GetById(long id)
	    => await GetById<SiteService>(id);
}
