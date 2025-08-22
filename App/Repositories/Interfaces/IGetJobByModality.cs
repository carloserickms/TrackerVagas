using App.DTOs;
using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IGetJobByModality<T>
    {
        Task<IEnumerable<T>> GetJobByModality(ModalityIdUserIdRequestDTO modalityIdUserId);
    }
}