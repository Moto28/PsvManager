using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;
using PsvManager.Infrastructure.Data.Interfaces;

namespace PsvManager.Infrastructure.Data.Repos
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly PsvContext _psvContext;
        private readonly DbSet<T> _dbSet;

        protected BaseRepository(PsvContext psvContext)
        {
            _psvContext = psvContext ?? throw new ArgumentNullException(nameof(psvContext));
            _dbSet = _psvContext.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _psvContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            _dbSet.Remove(entity);
            await _psvContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await _psvContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
