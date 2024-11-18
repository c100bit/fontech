using FonTech.Domain.Dto.Report;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Result;
using Microsoft.AspNetCore.Mvc;

namespace FonTech.Api.Controllers;

//[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class ReportController(IReportService service) : ControllerBase
{
    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetReport(long id)
    {
        var response = await service.GetReportByIdAsync(id);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpGet("reports/{userId:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> GetUserReports(long userId)
    {
        var response = await service.GetRepostsAsync(userId);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Delete(long id)
    {
        var response = await service.DeleteReportAsync(id);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Create([FromBody] CreateReportDto dto)
    {
        var response = await service.CreateReportAsync(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BaseResult<ReportDto>>> Update([FromBody] UpdateReportDto dto)
    {
        var response = await service.UpdateReportAsync(dto);
        if (response.IsSuccess) return Ok(response);
        return BadRequest(response);
    }
}