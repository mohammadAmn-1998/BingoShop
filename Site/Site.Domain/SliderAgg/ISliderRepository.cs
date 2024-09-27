using Shared.Domain;
using Site.Application.Contract.SliderApplication.Command;

namespace Site.Domain.SliderAgg
{
    public interface ISliderRepository 
    {
        EditSlider? GetForEdit(long id);

		Task<bool> ChangeActivation(long id);
		Task<bool> Create(Slider  slider);
		Task<bool> Edit(Slider  slider);
		Task<Slider?> GetById(long id);
			
    }
}
