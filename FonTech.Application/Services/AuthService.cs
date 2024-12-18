﻿using System.Security.Claims;
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

public class AuthService(
    IBaseRepository<User> repository,
    IBaseRepository<Role> roleRepository,
    IBaseRepository<UserRole> userRoleRepository,
    IUnitOfWork unitOfWork,
    ILogger logger,
    IMapper mapper,
    ITokenService tokenService,
    IBaseRepository<UserToken> tokenRepository) : IAuthService
{
    public async Task<BaseResult<UserDto>> Register(RegisterUserDto dto)
    {
        if (dto.Password != dto.PasswordConfirm)
            return new BaseResult<UserDto>
            {
                ErrorMessage = ErrorMessage.PasswordNotEqualsPasswordConfirm,
                ErrorCode = (int)ErrorCodes.PasswordNotEqualsPasswordConfirm
            };


        var user = await repository.GetAll().FirstOrDefaultAsync(u => u.Login == dto.Login);
        if (user != null)
            return new BaseResult<UserDto>
            {
                ErrorCode = (int)ErrorCodes.UserAlreadyExists,
                ErrorMessage = ErrorMessage.UserAlreadyExists
            };
        var hasUserPassword = HashPassword(dto.Password);

        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            user = new User
            {
                Login = dto.Login,
                Password = hasUserPassword
            };

            await unitOfWork.Users.CreateAsync(user);
            var role = await roleRepository.GetAll().FirstOrDefaultAsync(r => r.Name == "User");
            if (role == null)
                return new BaseResult<UserDto>
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };

            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = role.Id
            };
            await unitOfWork.UserRoles.CreateAsync(userRole);
            await transaction.CommitAsync();
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();
        }


        return new BaseResult<UserDto>
        {
            Data = mapper.Map<UserDto>(user)
        };
    }

    public async Task<BaseResult<TokenDto>> Login(LoginUserDto dto)
    {
        try
        {
            var user = await repository.GetAll().Include(x => x.Roles)
                .FirstOrDefaultAsync(u => u.Login == dto.Login);
            if (user == null)
                return new BaseResult<TokenDto>
                {
                    ErrorCode = (int)ErrorCodes.UserNotFound,
                    ErrorMessage = ErrorMessage.UserNotFound
                };
            if (!IsVerifiedPassword(dto.Password, user.Password))
                return new BaseResult<TokenDto>
                {
                    ErrorCode = (int)ErrorCodes.PasswordIsWrong,
                    ErrorMessage = ErrorMessage.PasswordIsWrong
                };
            var token = await tokenRepository.GetAll().FirstOrDefaultAsync(x => x.UserId == user.Id);
            var refreshToken = tokenService.GenerateRefreshToken();

            var userRoles = user.Roles;
            var claims = userRoles.Select(x => new Claim(ClaimTypes.Role, x.Name)).ToList();
            claims.Add(new Claim(ClaimTypes.Name, user.Login));

            var accessToken = tokenService.GenerateAccessToken(claims);
            if (token == null)
            {
                token = new UserToken
                {
                    UserId = user.Id,
                    RefreshToken = refreshToken,
                    RefreshTokenExpiryTime = DateTime.Now.AddDays(7)
                };
                await tokenRepository.CreateAsync(token);
            }
            else
            {
                token.RefreshToken = refreshToken;
                token.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            }

            return new BaseResult<TokenDto>
            {
                Data = new TokenDto
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                }
            };
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return new BaseResult<TokenDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }

    private static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
    }

    private static bool IsVerifiedPassword(string userPassword, string hashedPassword)
    {
        var hash = HashPassword(userPassword);
        return hash == hashedPassword;
    }
}