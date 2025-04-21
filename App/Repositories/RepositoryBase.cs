using App.DataBase;
using App.Repositories.Interfaces;

namespace App.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {

        protected readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public abstract Task Add(T entity);
        public abstract Task<T> GetById(Guid id);
        public abstract Task Delete(T entity);
    }
}