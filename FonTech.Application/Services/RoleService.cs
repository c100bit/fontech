using AutoMapper;
using FonTech.Application.Resources;
using FonTech.Domain.Dto.Role;
using FonTech.Domain.Entity;
using FonTech.Domain.Enum;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Result;
using Microsoft.EntityFrameworkCore;

namespace FonTech.Application.Services;

public class RoleService(
    IBaseRepository<User> userRepository,
    IBaseRepository<UserRole> userRoleRepository,
    IMapper mapper,
    IBaseRepository<Role> roleRepository) : IRoleService
{
    public async Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto)
    {
        var role = await roleRepository.GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
        if (role != null)
            return new BaseResult<RoleDto>
            {
                ErrorMessage = ErrorMessage.RoleAlreadyExists,
                ErrorCode = (int)ErrorCodes.RoleAlreadyExists
            };
        role = new Role
            {
                Name = dto.Name
            }
            ;
        await roleRepository.CreateAsync(role);
        return new BaseResult<RoleDto>
        {
            Data = mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> DeleteRoleAsync(long id)
    {
        var role = await roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
        if (role == null)
            return new BaseResult<RoleDto>
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };

        roleRepository.Remove(role);
        await roleRepository.SaveChangesAsync();
        return new BaseResult<RoleDto>
        {
            Data = mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto)
    {
        var role = await roleRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
        if (role == null)
            return new BaseResult<RoleDto>
            {
                ErrorMessage = ErrorMessage.RoleNotFound,
                ErrorCode = (int)ErrorCodes.RoleNotFound
            };

        roleRepository.Update(role);
        await roleRepository.SaveChangesAsync();
        return new BaseResult<RoleDto>
        {
            Data = mapper.Map<RoleDto>(role)
        };
    }

    public async Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto)
    {
        var user = await userRepository.GetAll().Include(x => x.Roles).FirstOrDefaultAsync(x => x.Login == dto.Login);

        if (user == null)
            return new BaseResult<UserRoleDto>
            {
                ErrorMessage = ErrorMessage.UserNotFound,
                ErrorCode = (int)ErrorCodes.UserNotFound
            };

        var roles = user.Roles.Select(x => x.Name).ToArray();

        if (roles.Any(x => x != dto.RoleName))
            return new BaseResult<UserRoleDto>
            {
                ErrorMessage = ErrorMessage.InternalServerError,
                ErrorCode = (int)ErrorCodes.InternalServerError
            };
        {
            var role = roleRepository.GetAll().FirstOrDefault(x => x.Name == dto.RoleName);
            if (role == null)
                return new BaseResult<UserRoleDto>
                {
                    ErrorMessage = ErrorMessage.RoleNotFound,
                    ErrorCode = (int)ErrorCodes.RoleNotFound
                };
            var userRole = new UserRole
            {
                RoleId = role.Id,
                UserId = user.Id
            };
            await userRoleRepository.CreateAsync(userRole);
            return new BaseResult<UserRoleDto>
            {
                Data = new UserRoleDto
                (
                    user.Login,
                    role.Name
                )
            };
        }
    }
}