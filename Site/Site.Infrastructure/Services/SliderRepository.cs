using Shared.Domain.SeedWorks.Base;

using Site.Application.Contract.SliderApplication.Command;
using Site.Domain.SliderAgg;

namespace Site.Infrastructure.Services;

internal class SliderRepository : BaseRepository, ISliderRepository
{

	public SliderRepository(SiteContext context) : base(context)
	{
	}

	public EditSlider? GetForEdit(long id) =>
       Table<Slider>().Select(s => new EditSlider
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName,
            Url = s.Url
        }).SingleOrDefault(s => s.Id == id);

	public async Task<bool> ChangeActivation(long id)
	{
		try
		{
			var slider =await GetById<Slider>(id);
			if (slider == null)
				throw new NullReferenceException();

			slider.ActivationChange();
			Update(slider);
			return await Save() > 0;


		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> Create(Slider slider)
	{
		try
		{
			Insert(slider);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> Edit(Slider slider)
	{
		try
		{
			Update(slider);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<Slider?> GetById(long id)
	 => await GetById<Slider>(id);
}
