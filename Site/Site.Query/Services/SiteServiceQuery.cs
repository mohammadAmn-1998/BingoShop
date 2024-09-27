using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shared.Application.Utility;
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.SiteServiceApplication.Query;
using Site.Domain.SiteServiceAgg;
using Site.Infrastructure;

namespace Site.Query.Services;

internal class SiteServiceQuery :BaseRepository ,ISiteServiceQuery
{
	public SiteServiceQuery(SiteContext context) : base(context)
	{
	}

	public List<SiteServiceAdminQueryModel> GetAllForAdmin() =>
		Table<SiteService>()
		.Select(s => new SiteServiceAdminQueryModel
		(s.Id,s.Title,s.ImageName,s.ImageAlt,s.CreateDate.ConvertToPersianDate(),s.Active)).ToList();

    public List<SiteServiceUIQueryModel> GetAllForUI() =>
        Table<SiteService>().Where(s=>s.Active)
        .Select(s => new SiteServiceUIQueryModel
        (s.Id, s.Title, s.ImageName, s.ImageAlt)).ToList();

  
}
