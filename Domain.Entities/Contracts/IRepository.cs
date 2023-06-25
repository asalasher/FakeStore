namespace FS.Domain.Entities.Contracts
{
    public interface IRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T?> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
