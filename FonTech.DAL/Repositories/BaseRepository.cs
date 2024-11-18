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
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        context.Update(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> RemoveAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        context.Remove(entity);
        await context.SaveChangesAsync();
        return entity;
    }
}