using Shared.Domain;
using Site.Application.Contract.MenuApplication.Command;

namespace Site.Domain.MenuAgg
{
    public interface IMenuRepository 
    {
        EditMenu? GetForEdit(long id);

		Task<Menu?> GetById(long id);

		Task<bool> ActivationChange(long id);

		Task<bool> Create(Menu menu);
		Task<bool> Edit(Menu menu);
    }
}
