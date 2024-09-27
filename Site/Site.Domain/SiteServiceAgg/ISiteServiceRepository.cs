using Shared.Domain;
using Site.Application.Contract.SiteServiceApplication.Command;
using Site.Application.Contract.SiteServiceApplication.Query;

namespace Site.Domain.SiteServiceAgg
{
    public interface ISiteServiceRepository 
    {
        EditSiteService? GetForEdit(long id);
        Task<bool> ChangeActivation(long id);
		Task<bool> Create(SiteService service);
		Task<bool> Edit(SiteService service);
		Task<SiteService?> GetById(long id);
    }

    
}
