namespace FonTech.Domain.Interfaces.Repositories;

public interface IBaseRepository<TEntity> : IStateSaveChanges where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity> CreateAsync(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);
}