using FonTech.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FonTech.DAL.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired().HasMaxLength(50);

        builder.HasData(new List<Role>
        {
            new()
            {
                Id = 1,
                Name = "Admin"
            },
            new()
            {
                Id = 2,
                Name = "User"
            }
        });
    }
}