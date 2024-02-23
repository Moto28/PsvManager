using Microsoft.EntityFrameworkCore;
using PsvManager.Infrastructure.Data.Contexts;

namespace PsvManager.Infrastructure.Data.Repos
{
    public abstract class BaseRepository<T> where T : class
    {
        //private readonly PsvContext _psvContext;

        //public BaseRepository(PsvContext psvContext)
        //{
        //    _psvContext = psvContext ?? throw new ArgumentNullException(nameof(psvContext));
        //}

        //public async Task AddAsync(T entity)
        //{
        //    await _psvContext.AddAsync(entity);
        //}

        //public async Task Delete(T entity)
        //{
        //    _psvContext.Remove(entity);
        //    await _psvContext.SaveChangesAsync();
        //}

        //public async Task<T?> GetByIdAsync(Guid id)
        //{
        //    return await _psvContext.FindAsync<T>(id);
        //}

        //public async Task Update(T entity)
        //{
        //    _psvContext.Update(entity);
        //    await _psvContext.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<T>> GetAllAsync()
        //{
        //    return await _psvContext.Set<T>().ToListAsync();
        //}
    }
}
