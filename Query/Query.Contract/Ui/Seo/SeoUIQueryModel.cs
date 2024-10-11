using Shared.Domain.Enums;

namespace Query.Contract.Ui.Seo;

public class SeoUIQueryModel
{

    public WhereSeo Where { get; private set; }
    public long OwnerId { get; private set; }
    public string MetaTitle { get; private set; }
    public string? MetaDescription { get; private set; }
    public string? MetaKeyWords { get; private set; }
    public bool IndexPage { get; private set; }
    public string? Canonical { get; private set; }
    public string? Schema { get; private set; }

    public SeoUIQueryModel()
    {

    }

    public SeoUIQueryModel(WhereSeo where, long ownerId, string metaTitle, string? metaDescription, string? metaKeyWords, bool indexPage, string? canonical, string? schema)
    {
        Where = where;
        OwnerId = ownerId;
        MetaTitle = metaTitle;
        MetaDescription = metaDescription;
        MetaKeyWords = metaKeyWords;
        IndexPage = indexPage;
        Canonical = canonical;
        Schema = schema;
    }
}