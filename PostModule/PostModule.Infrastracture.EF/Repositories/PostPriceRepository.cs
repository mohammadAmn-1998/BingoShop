using PostModule.Application.Contract.PostPriceApplication;
using PostModule.Domain.PostEntity;
using PostModule.Domain.Services;
using Shared.Infrastructure.BaseRepository;

namespace PostModule.Infrastracture.EF.Repositories
{
    internal class PostPriceRepository : Repository<int,PostPrice> , IPostPriceRepository
    {
        private readonly PostContext _context;
        public PostPriceRepository(PostContext context) : base(context)
        {
            _context = context;
        }

        public EditPostPrice GetForEdit(int id)
        {
            return _context.PostPrices.Select(p => new EditPostPrice
            {
                CityPrice = p.CityPrice,
                End = p.End,
                Id = p.Id,
                InsideStatePrice = p.InsideStatePrice,
                Start = p.Start,
                StateCenterPrice = p.StateCenterPrice,
                StateClosePrice = p.StateClosePrice,
                StateNonClosePrice = p.StateNonClosePrice,
                TehranPrice = p.TehranPrice
            }).SingleOrDefault(p => p.Id == id);
        }
    }
}
