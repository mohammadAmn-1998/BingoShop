using Seos.Application.Contract;
using Seos.Domain;
using Shared.Domain.Enums;
using Shared.Domain.SeedWorks.Base;


namespace Seos.Infrastructure
{
    internal class SeoRepository : BaseRepository , ISeoRepository
    {
        private readonly Seo_Context _context;
        public SeoRepository(Seo_Context context): base(context)
        {
            _context = context;
        }
       
        public Seo? GetSeo(long ownerId, WhereSeo where)
        {
            return Table<Seo>().SingleOrDefault(s => s.OwnerId == ownerId && s.Where == where);
        }

        public CreateSeo GetSeoForUbsert(long ownerId, WhereSeo where)
        {
            var seo = _context.Seos.SingleOrDefault(s => s.OwnerId == ownerId && s.Where == where);
            if (seo == null)
                return new()
                {
                    OwnerId = ownerId,
                    Where = where
                };

            return new()
            {
                OwnerId = seo.OwnerId,
                Canonical = seo.Canonical,
                IndexPage = seo.IndexPage,
                MetaDescription = seo.MetaDescription,
                MetaKeyWords = seo.MetaKeyWords,
                MetaTitle = seo.MetaTitle,
                Schema = seo.Schema,
                Where = seo.Where
            };
        }

        public async Task<Seo> GetSeoForUi(long ownerId, WhereSeo where, string title)
        {
            var seo = GetSeo(ownerId, where);
            if(seo == null)
            {
                seo = new Seo(title, "", "", true, "","", where, ownerId);
                Insert(seo);
                await Save();
            }
            return seo;
        }

        public async Task<bool> Create(CreateSeo command)
        {
	        try
	        {
		        Seo seo = new(command.MetaTitle, command.MetaDescription, command.MetaKeyWords, command.IndexPage,
			        command.Canonical, command.Schema, command.Where, command.OwnerId);

				Insert(seo);
				return await Save() > 0;
	        }
	        catch (Exception e)
	        {
		        return false;
	        }
        }
    }
}
