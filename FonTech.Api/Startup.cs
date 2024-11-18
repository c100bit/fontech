using System.Reflection;
using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace FonTech.Api;

public static class Startup
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddApiVersioning()
            .AddApiExplorer(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "FonTech API",
                Description = "FonTech API",
                TermsOfService = new Uri("https://ya.ru"),
                Contact = new OpenApiContact
                {
                    Email = "ya.ru",
                    Name = "Ya Ru",
                    Url = new Uri("https://ya.ru")
                }
            });
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "FonTech API",
                Description = "FonTech API",
                TermsOfService = new Uri("https://ya.ru"),
                Contact = new OpenApiContact
                {
                    Email = "ya.ru",
                    Name = "Ya Ru",
                    Url = new Uri("https://ya.ru")
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }
}