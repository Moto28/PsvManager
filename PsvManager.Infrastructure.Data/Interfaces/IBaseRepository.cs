namespace PsvManager.Infrastructure.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<Guid> DeleteAsync(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
