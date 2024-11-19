using FonTech.Domain.Entity;
using FonTech.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace FonTech.DAL.Repositories;

public class UnitOfWork(
    ApplicationDbContext context,
    IBaseRepository<User> users,
    IBaseRepository<Role> roles,
    IBaseRepository<UserRole> userRoles)
    : IUnitOfWork, IStateSaveChanges
{
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    public IBaseRepository<User> Users { get; init; } = users;
    public IBaseRepository<Role> Roles { get; init; } = roles;
    public IBaseRepository<UserRole> UserRoles { get; init; } = userRoles;

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await context.Database.BeginTransactionAsync();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}