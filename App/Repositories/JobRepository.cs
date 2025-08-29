using App.DataBase;
using App.DTOs;
using App.Models;
using App.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class JobRepository : RepositoryBase<JobVacancy>, IJobRepository
    {
        public JobRepository(AppDbContext context) : base(context)
        {
        }

        public async override Task Add(JobVacancy job)
        {
            _context.JobVacancy.Add(job);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<VacancyStatus>> AllStatus()
        {
            return await _context.VacancyStatus.ToListAsync();
        }

        public async Task AddVacacyStatus(VacancyStatus status)
        {
            _context.VacancyStatus.Add(status);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Modality>> AllModality()
        {
            return await _context.Modality.ToListAsync();
        }

        

        public async Task AddModality(Modality modality)
        {
            _context.Modality.Add(modality);
            await _context.SaveChangesAsync();
        }

        public async Task<Modality> GetModalityById(Guid modalityId)
        {
            return await _context.Modality.FirstAsync(m => m.Id == modalityId);
        }

        public async Task<VacancyStatus> GetStatusById(Guid statusId)
        {
            return await _context.VacancyStatus.FirstOrDefaultAsync(s => s.Id == statusId);
        }

        public async override Task Delete(JobVacancy job)
        {
            _context.JobVacancy.Remove(job);
            await _context.SaveChangesAsync();
        }

        public Task<IEnumerable<JobVacancy>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobVacancy>> GetAllById(UserPageRequestDTO userPage)
        {
            {
                const int maxPage = 6;
                int skip = (userPage.page - 1) * maxPage;

                var paginatedList = await _context.JobVacancy
                    .Where(j => j.UserId == userPage.userId)
                    .Skip(skip)
                    .Take(maxPage)
                    .ToListAsync();

                return paginatedList;
            }

            //return await _context.JobVacancy.Where(j => j.UserId == userPage.userId).ToListAsync();

        }

        public async override Task<JobVacancy> GetById(Guid id)
        {
            return await _context.JobVacancy.FirstOrDefaultAsync(j => j.Id == id);
        }

        public async override Task Edit(JobVacancy updatedJob)
        {
            _context.JobVacancy.Update(updatedJob);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<JobVacancy>> GetJobByTitle(SearchForUserJobs search)
        {
            return await _context.JobVacancy.Where(j => j.UserId == search.UserId && j.Title.Contains(search.JobTitle)).ToListAsync();
        }

        public async Task<IEnumerable<JobVacancy>> GetJobByModality(ModalityIdUserIdRequestDTO modalityIdUserId)
        {
            var modalityList = await _context.JobVacancy
                .Where(j => j.UserId == modalityIdUserId.UserId
                    && j.ModalityId == modalityIdUserId.ModalityId)
                .ToListAsync();

            return modalityList;
        }

        public async Task<TypeOfContract> GetTypeOfContract(Guid id)
        {
            return await _context.TypeOfContract.FirstAsync(t => t.Id == id);
        }

        public async Task<InterestLevel> GetInterestLevel(Guid id)
        {
            return await _context.InterestLevel.FirstAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<TypeOfContract>> AllTypeOfContract()
        {
            return await _context.TypeOfContract.ToListAsync();
        }

        public async Task<IEnumerable<InterestLevel>> AllInterestLevel()
        {
            return await _context.InterestLevel.ToListAsync();
        }
    }
}