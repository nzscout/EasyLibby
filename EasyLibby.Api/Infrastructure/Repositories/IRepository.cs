using EasyLibby.Api.Entities;
using System.Linq.Expressions;

namespace EasyLibby.Api.Infrastructure.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task CreateAsync(T entity);
    Task DeleteAsync(int id);
    Task<T?> GetAsync(int id);
    Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null);
    Task UpdateAsync(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);
}
