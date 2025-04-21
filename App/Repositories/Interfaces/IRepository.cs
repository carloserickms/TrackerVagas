
namespace App.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task Add(T entity);
        Task<T> GetById(Guid id);
        Task Delete(T entity);
    }
}