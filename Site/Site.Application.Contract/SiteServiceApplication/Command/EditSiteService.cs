namespace Site.Application.Contract.SiteServiceApplication.Command
{
    public class EditSiteService : CreateSiteService
    {
        public long Id { get; set; }
        public string ImageName { get; set; }
    }
}
