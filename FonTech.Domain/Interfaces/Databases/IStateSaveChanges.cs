namespace FonTech.Domain.Interfaces.Repositories;

public interface IStateSaveChanges
{
    public Task<int> SaveChangesAsync();
}