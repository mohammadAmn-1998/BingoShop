
using Shared.Domain.Enums;

namespace Site.Application.Contract.MenuApplication.Query
{
    public class MenuForAdminQueryModel
    {
        public long Id { get; set; }
        public long Number { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public MenuStatus Status { get; set; }
        public bool Active { get; set; }
        public string ImageName { get; set; }
        public string CreationDate { get; set; }
    }
}
