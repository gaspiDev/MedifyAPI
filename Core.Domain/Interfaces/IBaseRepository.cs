
using Core.Domain.Interfaces;

namespace Infrastructure.Data.Repositories
{
    public interface IBaseRepository<T> where T : class, IEntity
    {
        Task<T> CreateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>?> ReadAsync();
        Task<T?> ReadByIdAsync(Guid id);
        Task<T?> UpdateAsync(T entity);
    }
}