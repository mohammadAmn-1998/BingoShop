
using Shared.Application.Utility;
using Shared.Domain.Enums;
using Site.Application.Contract.SitePageApplication.Command;
using Site.Domain.SitePageAgg;


namespace Site.Application.Services;

internal class SitePageApplication : ISitePageApplication
{
	private readonly ISitePageRepository _sitePageRepository;

	public SitePageApplication(ISitePageRepository sitePageRepository)
	{
		_sitePageRepository = sitePageRepository;
	}

	public async Task<bool> ActivationChange(long id)
	=> await _sitePageRepository.ChangeActivation(id);

	public async Task<OperationResult> Create(CreateSitePage command)
	{
		if (await _sitePageRepository.ExistBy(c => c.Title == command.Title.Trim()))
			return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));
		 command.Slug = command.Slug.Trim().GenerateSlug();
		if (await _sitePageRepository.ExistBy(c => c.Slug == command.Slug))
			return new(Status.BadRequest, ErrorMessages.DuplicateSlugError, nameof(command.Slug));

		SitePage page = new(command.Title.Trim(), command.Slug, command.Text);
		if (await _sitePageRepository.Create(page)) return new(Status.Success);
		return new(Status.InternalServerError, ErrorMessages.InternalServerError, nameof(command.Title));
	}

	public async Task<OperationResult> Edit(EditSitePage command)
	{
		var page =await _sitePageRepository.GetById(command.Id);
		if (await _sitePageRepository.ExistBy(c => c.Title == command.Title.Trim() && c.Id != page.Id))
			return new(Status.BadRequest, ErrorMessages.DuplicateTitleError, nameof(command.Title));
		command.Slug = command.Slug.Trim().GenerateSlug();
		if (await _sitePageRepository.ExistBy(c => c.Slug == command.Slug && c.Id != page.Id))
			return new(Status.BadRequest, ErrorMessages.DuplicateSlugError, nameof(command.Slug));

		page.Edit(command.Title.Trim(), command.Slug, command.Text);
		if (await _sitePageRepository.Edit(page)) return new(Status.Success);
		return new(Status.BadRequest, ErrorMessages.InternalServerError);
	}

	public EditSitePage? GetForEdit(long id) =>
		_sitePageRepository.GetForEdit(id);
}
