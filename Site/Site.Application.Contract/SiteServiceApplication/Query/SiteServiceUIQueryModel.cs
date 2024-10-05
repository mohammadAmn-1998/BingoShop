namespace Site.Application.Contract.SiteServiceApplication.Query;

public class SiteServiceUIQueryModel
{
	public SiteServiceUIQueryModel(long id, string title, string imageName, string imageAlt)
	{
		Id = id;
		Title = title;
		ImageName = imageName;
		ImageAlt = imageAlt;
	}

	public long Id { get; private set; }
	public string Title { get; private set; }
	public string ImageName { get; private set; }
	public string ImageAlt { get; private set; }
}