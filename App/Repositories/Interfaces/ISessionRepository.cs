using App.Models;

namespace App.Repositories.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Task Add(Session session);
        Task<Session> GetById(Guid id);
        Task Delete(Session session);
    }
}