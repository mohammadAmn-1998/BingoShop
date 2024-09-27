
using Microsoft.EntityFrameworkCore;
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.BanerApplication.Query;
using Site.Domain.BanerAgg;
using Site.Infrastructure;

namespace Site.Query.Services;

internal class BanerQuery : BaseRepository, IBanerQuery
{
	public BanerQuery(SiteContext context) : base(context)
	{
	}

public List<BanerForAdminQueryModel> GetAllForAdmin()
    {
        return Table<Baner>().Select(b => new BanerForAdminQueryModel
        {
            Active = b.Active,
            CreationDate = b.CreateDate.ConvertToPersianDate(),
            Id  =b.Id,
            ImageName = b.ImageName,
            State = b.State,
            ImageAlt = b.ImageAlt,
        }).ToList();
    }

    public List<BanerForUiQueryModel> GetForUi(int count, BanerState state)
    {
        return Table<Baner>().Where(b => b.State == state && b.Active)
            .Select(b => new BanerForUiQueryModel
            {
                ImageAlt = b.ImageAlt,
                ImageName = b.ImageName,
                Url = b.Url
            }).Take(count).ToList();
    }

    
}