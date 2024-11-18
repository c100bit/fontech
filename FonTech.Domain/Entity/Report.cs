using FonTech.Domain.Interfaces;

namespace FonTech.Domain.Entity;

public class Report : IEntityId<long>, IAuditable
{
    public string Name { get; set; }
    public string Description { get; set; }
    public User User { get; init; }
    public long UserId { get; init; }

    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public long Id { get; set; }
}