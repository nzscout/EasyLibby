using EasyLibby.Api.Data;
using EasyLibby.Api.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyLibby.Api.Infrastructure.Repositories;

public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    private readonly LibraryDbContext dbContext;
    protected readonly ILogger<EfRepository<T>> logger;

    public EfRepository(LibraryDbContext dbContext, ILogger<EfRepository<T>> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task<IEnumerable<T>> GetAllAsync(int pageNumber, int pageSize, Expression<Func<T, bool>>? filter = null)
    {
        var skipCount = (pageNumber - 1) * pageSize;

        return await GetFiltered(filter)
                    .OrderBy(entity => entity.Id)
                    .Skip(skipCount)
                    .Take(pageSize)
                    .AsNoTracking().ToListAsync();
    }

    public async Task<int> CountAsync(Expression<Func<T, bool>>? filter)
    {
        return await GetFiltered(filter).CountAsync();
    }

    public async Task CreateAsync(T entity)
    {
        dbContext.Set<T>().Add(entity);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Created {Entity} id={Id}.", typeof(T).Name, entity.Id);
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Set<T>().Where(b => b.Id == id).ExecuteDeleteAsync();
        logger.LogInformation("Deleted {Entity} id={Id}.", typeof(T).Name, id);
    }

    public async Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        await dbContext.SaveChangesAsync();
        logger.LogInformation("Updated {Entity}, id={id}.", typeof(T).Name, entity.Id);
    }

    public async Task<T?> GetAsync(int id)
    {
        return await dbContext.Set<T>().FindAsync(id);
    }

    protected virtual IQueryable<T> GetFiltered(Expression<Func<T, bool>>? filter)
    {
        if (filter is null)
        {
            return dbContext.Set<T>();
        }

        return dbContext.Set<T>().Where(filter);
    }
}
