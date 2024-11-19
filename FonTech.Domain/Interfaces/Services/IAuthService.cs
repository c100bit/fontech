using FonTech.Domain.Dto;
using FonTech.Domain.Dto.User;
using FonTech.Domain.Result;

namespace FonTech.Domain.Interfaces.Services;

/// <summary>
///     Register user service
/// </summary>
public interface IAuthService
{
    Task<BaseResult<UserDto>> Register(RegisterUserDto dto);
    Task<BaseResult<TokenDto>> Login(LoginUserDto dto);
}