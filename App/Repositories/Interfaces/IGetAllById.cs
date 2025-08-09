using App.DTOs;
using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IGetAllById<T>
    {
        Task<IEnumerable<T>> GetAllById(UserPageRequestDTO userPage);
    }
}