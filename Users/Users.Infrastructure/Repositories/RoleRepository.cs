using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.BaseRepository;
using Users.Domain.UserAgg;
using Users.Infrastructure.Context;

namespace Users.Infrastructure.Repositories;

internal class RoleRepository : Repository<int, Role> , IRoleRepository
{
	public RoleRepository(BlogUsers_Context context) : base(context)
	{
	}
}