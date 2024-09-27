using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.SliderApplication.Command
{
    public interface ISliderApplication
    {
        Task<OperationResult> Create(CreateSlider command);
        Task<OperationResult> Edit(EditSlider command);
        Task<bool> ActivationChange(long id);
        EditSlider? GetForEdit(long id);
    }
}
