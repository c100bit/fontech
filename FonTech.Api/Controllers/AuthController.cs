using FonTech.Domain.Dto;
using FonTech.Domain.Dto.User;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("register")]
    public async Task<ActionResult<BaseResult>> Register([FromBody] RegisterUserDto dto)
    {
        var response = await authService.Register(dto);

        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpPost("login")]
    public async Task<ActionResult<BaseResult<TokenDto>>> Login([FromBody] LoginUserDto dto)
    {
        var response = await authService.Login(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }
}