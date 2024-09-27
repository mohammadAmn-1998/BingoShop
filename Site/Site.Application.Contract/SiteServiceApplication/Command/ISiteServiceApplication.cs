using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.SiteServiceApplication.Command
{
    public interface ISiteServiceApplication
    {
        Task<OperationResult> Create(CreateSiteService commmand);
        Task<OperationResult> Edit(EditSiteService commmand);
        Task<bool> ActivationChange(long id);
        EditSiteService? GetForEdit(long id);
    }
}
