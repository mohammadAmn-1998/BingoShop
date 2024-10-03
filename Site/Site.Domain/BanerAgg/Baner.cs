

using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;

namespace Site.Domain.BanerAgg
{
    public class Baner : BaseEntityActive<long>
    {
        public Baner(string imageName, string imageAlt, string url, BanerState state)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Url = url;
            State = state;
        }
        public void Edit(string imageName, string imageAlt, string url,BanerState state)
        {
            ImageName = imageName;
            ImageAlt = imageAlt;
            Url = url;
            State = state;
        }
        public string ImageName { get; private set; }
        public string ImageAlt { get; private set; }
        public string Url { get; private set; }
        public BanerState State { get; private set; }
    }
}
