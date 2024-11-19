namespace FonTech.Domain.Settings;

public class JwtSettings
{
    public const string DefaultSection = "Jwt";
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string JwtKey { get; set; }
    public string JwtIssuer { get; set; }
    public string RefreshTokenValidityInDays { get; set; }
    public string LifeTime { get; set; }
    public string Authority { get; set; }
}