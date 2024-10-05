namespace Site.Application.Contract.SiteServiceApplication.Query;
public interface ISiteServiceQuery
{
	List<SiteServiceAdminQueryModel> GetAllForAdmin();
	List<SiteServiceUIQueryModel> GetAllForUI();
}
public class SiteServiceAdminQueryModel
{
	public SiteServiceAdminQueryModel(long id, string title, string imageName, string imageAlt, string creationDate, bool active)
	{
		Id = id;
		Title = title;
		ImageName = imageName;
		ImageAlt = imageAlt;
		CreationDate = creationDate;
		Active = active;
	}

	public long Id { get; private set; }
    public string Title { get; private set; }
	public string ImageName { get; private set; }
	public string ImageAlt { get; private set; }
	public string CreationDate { get; private set; }
	public bool Active { get; private set; }
}