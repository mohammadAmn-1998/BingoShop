using Microsoft.EntityFrameworkCore;
using Shared.Domain.SeedWorks.Base;
using Users1.Domain.UserAgg;
using Users1.Domain.UserAgg.IRepositories;
using Users1.Infrastructure.Context;

namespace Users1.Infrastructure.Repositories;

internal class RoleRepository : BaseRepository, IRoleRepository
{
	public RoleRepository(UsersContext context) : base(context)
	{
	}

	
}