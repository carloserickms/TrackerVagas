using App.DataBase;
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
            return await _context.VacancyStatus.FirstAsync(s => s.Id == statusId);
        }

        public async override Task Delete(JobVacancy job)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<JobVacancy>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JobVacancy>> GetAllById(Guid jobId)
        {
            return await _context.JobVacancy.Where(j => j.UserId == jobId).ToListAsync();
        }

        public async override Task<JobVacancy> GetById(Guid id)
        {
            return await _context.JobVacancy.FirstOrDefaultAsync(j => j.Id == id);
        }
    }
}