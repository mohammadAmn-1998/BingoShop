using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.BanerApplication.Command
{
    public interface IBanerApplication
    {
        Task<OperationResult> Create(CreateBaner command);
        Task<OperationResult> Edit(EditBaner command);
        Task<bool> ActivationChange(long id);
        EditBaner? GetForEdit(long id);
    }
}
