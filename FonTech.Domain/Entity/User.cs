using FonTech.Domain.Interfaces;

namespace FonTech.Domain.Entity;

public class User : IEntityId<long>, IAuditable
{
    public string Login { get; init; }
    public string Password { get; init; }
    public List<Report> Reports { get; init; }
    public UserToken UserToken { get; set; }

    public DateTime CreatedAt { get; set; }
    public long CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public long Id { get; set; }
}