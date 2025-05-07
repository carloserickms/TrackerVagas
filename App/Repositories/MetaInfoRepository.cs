using App.DataBase;
using App.Models;
using App.Repositories.Interfaces;

namespace App.Repositories
{
    public class MetaInfoRepository : RepositoryBase<MetaInfo>, IMetaInfoRepository
    {
        public MetaInfoRepository(AppDbContext context) : base(context)
        {
        }

        public override async Task Add(MetaInfo metaInfo)
        {
            _context.MetaInfo.Add(metaInfo);
            await _context.SaveChangesAsync();
        }

        public override Task Delete(MetaInfo metaInfo)
        {
            throw new NotImplementedException();
        }

        public override Task<MetaInfo> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}