
using Shared.Domain.SeedWorks.Base;
using Site.Application.Contract.MenuApplication.Command;
using Site.Domain.MenuAgg;

namespace Site.Infrastructure.Services;

internal class MenuRepository :BaseRepository, IMenuRepository
{
	public MenuRepository(SiteContext context) : base(context)
    {
       
    }

    public EditMenu? GetForEdit(long id) =>
       Table<Menu>().Select(s => new EditMenu
        {
            ImageAlt = s.ImageAlt,
            Id = s.Id,
            ImageFile = null,
            ImageName = s.ImageName,
            Number  =s.Number,
            Title = s.Title,
            ParentId = s.ParentId == null ? 0 : s.ParentId.Value, 
            Url = s.Url
        }).SingleOrDefault(s => s.Id == id);

    public async Task<Menu?> GetById(long id)
    {

	    return await GetById<Menu>(id);
    }

    public async Task<bool> ActivationChange(long id)
    {

	    try
	    {

		    var menu =await GetById<Menu>(id);
		    if (menu == null)
			    throw new NullReferenceException();

			menu.ActivationChange();
			Update(menu);
			return await Save() > 0;	

	    }
	    catch (Exception e)
	    {
		    return false;
	    }
    }

    public async Task<bool> Create(Menu menu)
    {
	    try
	    {
			Insert(menu);
			return await Save() > 0;
	    }
	    catch (Exception e)
	    {
		    return false;
	    }
    }

    public async Task<bool> Edit(Menu menu)
    {
		try
		{
			Update(menu);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}

}
}
