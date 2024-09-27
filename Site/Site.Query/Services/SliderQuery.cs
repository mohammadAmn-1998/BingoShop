using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Application;
using Shared.Application.Utility;
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.SliderApplication.Query;
using Site.Domain.SliderAgg;
using Site.Infrastructure;

namespace Site.Query.Services;

internal class SliderQuery : BaseRepository, ISliderQuery
{
    

    public SliderQuery(SiteContext context ) : base(context)
    {
        
    }

    public List<SliderForAdmin> GetAllForAdmin()
    {
        return Table<Slider>().Select(s => new SliderForAdmin
        {
            Active = s.Active,
            ImageAlt = s.ImageAlt,
            CreationDate = s.CreateDate.ConvertToPersianDate(),
            Id = s.Id,
            ImageName = s.ImageName
        }).ToList();

    }

    public List<SliderForUi> GetAllForUi()
    {
        return Table<Slider>().Where(s => s.Active)
            .Select(s => new SliderForUi()
            {
                ImageAlt = s.ImageAlt,
                ImageName = s.ImageName,
                Url = s.Url
            }).ToList();
    }
}
