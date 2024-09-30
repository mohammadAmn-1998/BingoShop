using Shared.Domain.Enums;

namespace Query.Contract.Admin.Seo;

public interface ISeoAdminQuery
{
	Task<string> GetAdminSeoTitle(WhereSeo where, long ownerId);
}