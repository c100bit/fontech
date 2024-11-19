using FonTech.Domain.Interfaces.Repositories;

namespace FonTech.DAL.Repositories;

public class BaseRepository<TEntity>(ApplicationDbContext context) : IBaseRepository<TEntity>
    where TEntity : class
{
    public IQueryable<TEntity> GetAll()
    {
        return context.Set<TEntity>();
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        await context.AddAsync(entity);
        return entity;
    }


    public TEntity Update(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        context.Update(entity);
        return entity;
    }

    public TEntity Remove(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        context.Remove(entity);
        return entity;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}