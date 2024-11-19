using FonTech.Domain.Entity;
using Microsoft.EntityFrameworkCore.Storage;

namespace FonTech.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable

{
    IBaseRepository<User> Users { get; init; }
    IBaseRepository<Role> Roles { get; init; }
    IBaseRepository<UserRole> UserRoles { get; init; }

    Task<IDbContextTransaction> BeginTransactionAsync();
    Task<int> SaveChangesAsync();
}