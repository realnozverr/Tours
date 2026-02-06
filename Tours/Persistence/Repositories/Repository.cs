using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Tours.Persistence.Repositories;

public class Repository<T>(DataContext context) : IRepository<T> where T : class
{
    // Получаем конкретную сущность на T, например context.Set<T> где Т будет Tour или Client
    private readonly DbSet<T> _dbSet = context.Set<T>();
    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();

    // Получение по id, у всех сущностей id int
    public async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

    // Добавление сущности
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    // обновление сущности
    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync();
    }  

    // удаление сущности
    public async Task DeleteAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await context.SaveChangesAsync();
        }
    }

    // получение всех сущностей с каким то параметром с помощью методов расширений LINQ
    public async Task<List<T>> FindAllAsync(Expression<Func<T, bool>> predicate) => await _dbSet.Where(predicate).ToListAsync();
}