using App.DTOs;
using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IGetJobByTitle<T>
    {
        Task<IEnumerable<T>> GetJobByTitle(SearchForUserJobs search);
    }
}