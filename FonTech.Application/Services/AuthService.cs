using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using FonTech.Application.Resources;
using FonTech.Domain.Dto;
using FonTech.Domain.Dto.User;
using FonTech.Domain.Entity;
using FonTech.Domain.Enum;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FonTech.Application.Services;

public class AuthService(IBaseRepository<User> repository, ILogger logger, IMapper mapper) : IAuthService
{
    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
            return new BaseResult<UserDto>
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PaswordNotEqualsPasswordConfirm
            };

        try
        {
            var user = await repository.GetAll().FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user != null)
                return new BaseResult<UserDto>
                {
                    ErrorCode = (int)ErrorCodes.UserAlreadyExists,
                    ErrorMessage = ErrorMessage.UserAlreadyExists
                };
            var hasUserPassword = HashPassword(dto.Password);
            user = new User
            {
                Login = dto.Login,
                Password = hasUserPassword
            };
            await repository.CreateAsync(user);
            return new BaseResult<UserDto>
            {
                Data = mapper.Map<UserDto>(user)
            };
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return new BaseResult<UserDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }

    public Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        throw new NotImplementedException();
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(bytes).ToLower();
    }
}