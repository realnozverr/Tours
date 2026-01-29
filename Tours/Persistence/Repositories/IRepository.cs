using System.Linq.Expressions;

namespace Tours.Persistence.Repositories;

public interface IRepository<T>
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(int id);
    Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate);
}