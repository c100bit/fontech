using System.Net.Mime;
using FonTech.Domain.Dto.Role;
using FonTech.Domain.Entity;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

/// <summary>
/// </summary>
/// <param name="service"></param>
[Consumes(MediaTypeNames.Application.Json)]
[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class RoloeController(IRoleService service) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Create([FromBody] CreateRoleDto dto)
    {
        var response = await service.CreateRoleAsync(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Update([FromBody] RoleDto dto)
    {
        var response = await service.UpdateRoleAsync(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    /// <summary>
    ///     Request for update report:
    ///     PUT
    ///     {
    ///     "name" "Report1",
    ///     "description": "Test"
    ///     }
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200">Если роль обновился</response>
    /// <response code="400">Если роль не была обновлена</response>
    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> Delete(long id)
    {
        var response = await service.DeleteRoleAsync(id);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    /// <summary>
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost("addRole")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<Role>>> AddRoleForUser([FromBody] UserRoleDto dto)
    {
        var response = await service.AddRoleForUserAsync(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }
}