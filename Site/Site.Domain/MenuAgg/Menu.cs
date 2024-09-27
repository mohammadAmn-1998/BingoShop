
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Site.Domain.MenuAgg
{
    public class Menu : BaseEntityActive<long>
    {
        public Menu(long number, string title, string url, MenuStatus status,
             string? imageName, string? imageAlt, long? parentId)
        {
            Number = number;
            Title = title;
            Url = url;
            Status = status;
            Active = true;
            ImageName = imageName;
            ImageAlt = imageAlt;
            ParentId = parentId;
        }
        public void Edit(long number, string title, string url, 
             string? imageName, string? imageAlt)
        {
            Number = number;
            Title = title;
            Url = url;
            Active = true;
            ImageName = imageName;
            ImageAlt = imageAlt;
        }
        
        public Menu()
        {
            Parent = null;
            Childs = new();
        }
        public long Number { get; private set; }
        public string Title { get; private set; }
        public string Url { get; private set; }
        public MenuStatus Status { get; private set; }
        public bool Active { get; private set; }
        public string? ImageName { get; private set; }
        public string? ImageAlt { get; private set; }
        public long? ParentId { get; private set; }
        public Menu? Parent { get; private set; }
        public List<Menu>? Childs { get; private set; }
    }
}
