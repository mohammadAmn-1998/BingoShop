using Shared.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Application.Utility;

namespace Site.Application.Contract.MenuApplication.Command
{
    public interface IMenuApplication
    {
        Task<OperationResult> Create(CreateMenu command);
        Task<CreateSubMenu> GetForCreate(long parentId);
        Task<OperationResult> CreateSub(CreateSubMenu command);
        Task<OperationResult> Edit(EditMenu command);
        EditMenu? GetForEdit(long id);
        Task<bool> ActivationChange(long id);
    }
}
