using System.Linq.Expressions;
using Shared.Domain;
using Site.Application.Contract.SitePageApplication.Command;

namespace Site.Domain.SitePageAgg
{
	public interface ISitePageRepository
	{
        SitePage? GetBySlug(string slug);
        EditSitePage? GetForEdit(long id);

        Task<bool> ExistBy(Expression<Func<SitePage, bool>> expression);
		Task<SitePage?> GetById(long id);

		Task<bool> Create(SitePage sitePage);
		Task<bool> Edit(SitePage sitePage);

		Task<bool> ChangeActivation(long id);
	}
}
