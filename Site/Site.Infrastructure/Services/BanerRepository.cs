using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;

using Site.Application.Contract.BanerApplication.Command;
using Site.Domain.BanerAgg;

namespace Site.Infrastructure.Services;

internal class BanerRepository : BaseRepository, IBanerRepository
{
	public BanerRepository(SiteContext context) : base(context)
	{
	}


	public EditBaner? GetForEdit(long id) =>
		Table<Baner>().Select(s => new EditBaner
		{
			ImageAlt = s.ImageAlt,
			Id = s.Id,
			ImageFile = null,
			ImageName = s.ImageName,
			Url = s.Url,
			State = s.State
		}).SingleOrDefault(s => s.Id == id);

	public async Task<bool> ChangeActivation(long id)
	{
		try
		{
			var baner = await GetById<Baner>(id);
			if (baner == null)
				throw new NullReferenceException();

			baner.ActivationChange();
			Update(baner);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public Baner? GetById(long id)
		=> Table<Baner>().SingleOrDefault(x => x.Id == id);

	public async Task<bool> Create(CreateBaner command)
	{

		try
		{
			Baner baner = new Baner(command.ImageName!, command.ImageAlt, command.Url, command.State);
			Insert(baner);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}

	public async Task<bool> Edit(EditBaner command)
	{
		try
		{
			var baner = await GetById<Baner>(command.Id);
			if (baner == null)
				throw new NullReferenceException();

			baner.Edit(command.ImageName,command.ImageAlt,command.Url,command.State);
			Update(baner);
			return await Save() > 0;
		}
		catch (Exception e)
		{
			return false;
		}
	}
}
