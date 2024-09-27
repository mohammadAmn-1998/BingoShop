using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shared.Application.Utility;
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.SitePageApplication.Query;
using Site.Domain.SitePageAgg;
using Site.Infrastructure;

namespace Site.Query.Services
{
	internal class SitePageQuery :BaseRepository, ISitePageQuery
	{
		public SitePageQuery(SiteContext context) : base(context)
		{
		}

		public List<SitePageAdminQueryModel> GetAllForAdmin() =>
			Table<SitePage>().Select(p => new SitePageAdminQueryModel
			{
				Active = p.Active,
				CreateDate = p.CreateDate.ConvertToPersianDate(),
				Id = p.Id,
				Slug = p.Slug,
				Title = p.Title,
				UpdateDate = p.UpdateDate.ConvertToPersianDate()
			}).ToList();

		
	}
}
