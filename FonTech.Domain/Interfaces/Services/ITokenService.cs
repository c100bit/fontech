using System.Security.Claims;
using FonTech.Domain.Dto;
using FonTech.Domain.Result;

namespace FonTech.Domain.Interfaces.Services;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    Task<BaseResult<TokenDto>> RefreshTokenAsync(TokenDto dto);
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
}