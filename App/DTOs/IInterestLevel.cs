
namespace App.Repositories.Interfaces
{
    public interface IInterestLevel<T>
    {
        Task<T> GetInterestLevel(Guid id);
    }
}
