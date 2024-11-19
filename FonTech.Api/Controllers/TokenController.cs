using FonTech.Domain.Dto;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

/// <summary>
/// </summary>
/// <param name="service"></param>
[ApiController]
public class TokenController(ITokenService service) : Controller
{
    [HttpPost("refresh")]
    public async Task<ActionResult<BaseResult<TokenDto>>> RefreshToken([FromBody] TokenDto tokenDto)
    {
        var response = await service.RefreshTokenAsync(tokenDto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }
}