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