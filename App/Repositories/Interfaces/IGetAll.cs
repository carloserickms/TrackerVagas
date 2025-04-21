using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IGetAll<T>
    {
        Task<IEnumerable<T>> GetAll();
    }
}