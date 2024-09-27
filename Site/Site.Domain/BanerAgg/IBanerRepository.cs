

using Site.Application.Contract.BanerApplication.Command;

namespace Site.Domain.BanerAgg
{
    public interface IBanerRepository 
    {
        EditBaner? GetForEdit(long id);
		Task<bool> ChangeActivation(long id);
		Baner? GetById(long id);

		Task<bool> Create(CreateBaner command);
		Task<bool> Edit(EditBaner command);
    }
}
