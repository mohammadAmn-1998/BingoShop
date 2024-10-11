using Shared.Domain.Enums;

namespace Query.Contract.Ui.Seo;

public interface ISeoUIQuery
{
	Task<SeoUIQueryModel> GetSeoForUI(long ownerId, WhereSeo where,string title);
}