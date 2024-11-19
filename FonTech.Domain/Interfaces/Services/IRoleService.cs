using FonTech.Domain.Dto.Role;
using FonTech.Domain.Result;

namespace FonTech.Domain.Interfaces.Services;

/// <summary>
///     roles manager
/// </summary>
public interface IRoleService
{
    Task<BaseResult<RoleDto>> CreateRoleAsync(CreateRoleDto dto);
    Task<BaseResult<RoleDto>> DeleteRoleAsync(long id);
    Task<BaseResult<RoleDto>> UpdateRoleAsync(RoleDto dto);
    Task<BaseResult<UserRoleDto>> AddRoleForUserAsync(UserRoleDto dto);
}