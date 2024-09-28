using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace PostModule.Application.Contract.PostSettingApplication.Command
{
    public interface IPostSettingApplication
    {
        UbsertPostSetting GetForUbsert();
        OperationResult Ubsert(UbsertPostSetting command);
    }
}
