namespace PsvManager.Infrastructure.Data.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task Delete(T entity);
        Task<T?> GetByIdAsync(Guid id);
        Task Update(T entity);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
