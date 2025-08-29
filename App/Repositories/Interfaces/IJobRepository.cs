using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IJobRepository : IRepository<JobVacancy>, IGetAll<JobVacancy>, IGetAllById<JobVacancy>,
    IGetJobByTitle<JobVacancy>, IGetJobByModality<JobVacancy>, ITypeOfContract<TypeOfContract>, IInterestLevel<InterestLevel>
    {
        Task Add(JobVacancy entity);
        Task<JobVacancy> GetById(Guid id);
        Task Delete(JobVacancy entity);
        Task<IEnumerable<VacancyStatus>> AllStatus();
        Task AddVacacyStatus(VacancyStatus status);
        Task<IEnumerable<Modality>> AllModality();
        Task AddModality(Modality modality);
        Task<IEnumerable<TypeOfContract>> AllTypeOfContract();
        Task<IEnumerable<InterestLevel>> AllInterestLevel();
        Task<Modality> GetModalityById(Guid id);
        Task<VacancyStatus> GetStatusById(Guid id);
    }
}