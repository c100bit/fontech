using AutoMapper;
using FonTech.Application.Resources;
using FonTech.Domain.Dto.Report;
using FonTech.Domain.Entity;
using FonTech.Domain.Enum;
using FonTech.Domain.Interfaces.Repositories;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Interfaces.Validations;
using FonTech.Domain.Result;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace FonTech.Application.Services;

public class ReportService(
    IBaseRepository<Report> reportRepository,
    IBaseRepository<User> userRepository,
    IReportValidator reportValidator,
    IMapper mapper,
    ILogger logger) : IReportService
{
    /// <inheritdoc />
    public async Task<CollectionResult<ReportDto>> GetRepostsAsync(long userId)
    {
        ReportDto[] reports;
        try
        {
            reports = await reportRepository.GetAll()
                .Where(r => r.UserId == userId)
                .Select(r => new ReportDto(r.Id, r.Name, r.Description, r.CreatedAt.ToLongDateString()))
                .ToArrayAsync();
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return new CollectionResult<ReportDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }

        if (reports.Length != 0)
            return new CollectionResult<ReportDto>
            {
                Data = reports,
                Count = reports.Length
            };
        logger.Warning(ErrorMessage.ReportsNotFound, reports.Length);
        return new CollectionResult<ReportDto>
        {
            ErrorCode = (int)ErrorCodes.ReportsNotFound,
            ErrorMessage = ErrorMessage.ReportsNotFound
        };
    }

    /// <inheritdoc />
    public Task<BaseResult<ReportDto>> GetReportByIdAsync(long id)
    {
        ReportDto? report;
        try
        {
            report = reportRepository.GetAll()
                .AsEnumerable()
                .Select(x => new ReportDto(x.Id, x.Name, x.Description, x.CreatedAt.ToLongDateString()))
                .FirstOrDefault(x => x.Id == id);
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return Task.FromResult(new BaseResult<ReportDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            });
        }

        if (report != null)
            return Task.FromResult(new BaseResult<ReportDto>
            {
                Data = report
            });

        logger.Warning($"Отчет с {id} не найден", id);
        return Task.FromResult(new BaseResult<ReportDto>
        {
            ErrorCode = (int)ErrorCodes.ReportNotFound,
            ErrorMessage = ErrorMessage.ReportNotFound
        });
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> CreateReportAsync(CreateReportDto dto)
    {
        try
        {
            var user = await userRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == dto.UserId);
            var report = await reportRepository
                .GetAll().FirstOrDefaultAsync(x => x.Name == dto.Name);
            var result = reportValidator.CreateValidator(report, user);
            if (!result.IsSuccess)
                return new BaseResult<ReportDto>
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            report = new Report
            {
                Name = dto.Name,
                Description = dto.Description,
                UserId = user!.Id
            };
            await reportRepository.CreateAsync(report);
            return new BaseResult<ReportDto>
            {
                Data = mapper.Map<ReportDto>(report)
            };
        }

        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return new BaseResult<ReportDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }

    /// <inheritdoc />
    public async Task<BaseResult<ReportDto>> DeleteReportAsync(long id)
    {
        try
        {
            var report = await reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
            var result = reportValidator.ValidateOnNull(report);
            if (!result.IsSuccess)
                return new BaseResult<ReportDto>
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };

            await reportRepository.RemoveAsync(report!);
            return new BaseResult<ReportDto>
            {
                Data = mapper.Map<ReportDto>(report)
            };
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return new BaseResult<ReportDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }

    public async Task<BaseResult<ReportDto>> UpdateReportAsync(UpdateReportDto dto)
    {
        try
        {
            var report = await reportRepository.GetAll().FirstOrDefaultAsync(x => x.Id == dto.Id);
            var result = reportValidator.ValidateOnNull(report);
            if (!result.IsSuccess)
                return new BaseResult<ReportDto>
                {
                    ErrorCode = result.ErrorCode,
                    ErrorMessage = result.ErrorMessage
                };
            report!.Name = dto.Name;
            report.Description = dto.Description;
            await reportRepository.UpdateAsync(report);

            return new BaseResult<ReportDto>
            {
                Data = mapper.Map<ReportDto>(report)
            };
        }
        catch (Exception e)
        {
            logger.Error(e, e.Message);
            return new BaseResult<ReportDto>
            {
                ErrorCode = (int)ErrorCodes.InternalServerError,
                ErrorMessage = ErrorMessage.InternalServerError
            };
        }
    }
}