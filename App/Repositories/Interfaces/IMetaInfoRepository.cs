using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IMetaInfoRepository : IRepository<MetaInfo>
    {
        Task Add(MetaInfo session);
        Task<MetaInfo> GetById(Guid id);
        Task Delete(MetaInfo session);
    }
}