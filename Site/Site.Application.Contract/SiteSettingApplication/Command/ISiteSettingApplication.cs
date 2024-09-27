using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.SiteSettingApplication.Command
{
    public interface ISiteSettingApplication
    {
        Task<OperationResult> Ubsert(UbsertSiteSetting command);
        Task<UbsertSiteSetting> GetForUbsert();
    }
}
