﻿using System.Reflection;
using FluentValidation;
using FonTech.Application.Services;
using FonTech.Application.Validations;
using FonTech.Application.Validations.FluentValidations.Report;
using FonTech.Domain.Dto.Report;
using FonTech.Domain.Interfaces.Services;
using FonTech.Domain.Interfaces.Validations;
using Microsoft.Extensions.DependencyInjection;

namespace FonTech.Application.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        InitServices(services);
    }

    private static void InitServices(IServiceCollection services)
    {
        services.AddScoped<IReportValidator, ReportValidator>();
        services.AddScoped<IValidator<CreateReportDto>, CreateReportValidator>();
        services.AddScoped<IValidator<UpdateReportDto>, UpdateReportValidator>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IRoleService, RoleService>();
    }
}