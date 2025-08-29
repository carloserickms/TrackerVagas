
namespace App.Repositories.Interfaces
{
    public interface ITypeOfContract<T>
    {
        Task<T> GetTypeOfContract(Guid id);
    }
}
