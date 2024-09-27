using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;

using Site.Application.Contract.SitePageApplication.Command;
using Site.Domain.SitePageAgg;

namespace Site.Infrastructure.Services;

internal class SitePageRepository : BaseRepository, ISitePageRepository
{
	

	public SitePageRepository(SiteContext context) : base(context)
	{
		
	}

    public SitePage? GetBySlug(string slug)
    {
		return Table<SitePage>().SingleOrDefault(s => s.Slug.Trim().ToLower() == slug.Trim().ToLower());
    }

    public EditSitePage? GetForEdit(long id) =>
		Table<SitePage>().Select(s => new EditSitePage
		{
			Id = s.Id,
            Slug = s.Slug,
            Text = s.Description,
            Title = s.Title
		}).SingleOrDefault(s => s.Id == id);

    public async Task<bool> ExistBy(Expression<Func<SitePage, bool>> expression)
	    => await Table<SitePage>().AnyAsync(expression);

    public async Task<SitePage?> GetById(long id)
	    => await GetById<SitePage>(id);
    public async Task<bool> Create(SitePage sitePage)
    {
	    try
	    {
		   Insert(sitePage);
		   return await Save() > 0;
	    }
	    catch (Exception e)
	    {
		    return false;
	    }
    }

    public async Task<bool> Edit(SitePage sitePage)
    {
		try
		{
			Update(sitePage);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
}

    public async Task<bool> ChangeActivation(long id)
    {
	    try
	    {
		    var sitePage =await GetById<SitePage>(id);
		    if (sitePage == null)
			    throw new NullReferenceException();

			sitePage.ActivationChange();
			Update(sitePage);
			return await Save() > 0;
	    }
	    catch (Exception re)
	    {
		    return false;
	    }
    }
}
