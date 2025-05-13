using App.Models;

namespace App.Repositories.Interfaces
{
    public interface IJobRepository : IRepository<JobVacancy>, IGetAll<JobVacancy>, IGetAllById<JobVacancy>
    {
        Task Add(JobVacancy entity);
        Task<JobVacancy> GetById(Guid id);
        Task Delete(JobVacancy entity);
        Task<IEnumerable<VacancyStatus>> AllStatus();
        Task AddVacacyStatus(VacancyStatus status);
        Task<IEnumerable<Modality>> AllModality();
        Task AddModality(Modality modality);
    }
}