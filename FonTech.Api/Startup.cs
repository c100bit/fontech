using System.Reflection;
using System.Text;
using Asp.Versioning;
using FonTech.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FonTech.Api;

public static class Startup
{
    /// <summary>
    ///     Auth and auth
    /// </summary>
    /// <param name="services"></param>
    /// <param name="builder"></param>
    public static void AddAuthenticationAndAuthorization(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            var options = builder.Configuration.GetSection(JwtSettings.DefaultSection).Get<JwtSettings>();
            var jwtKey = options.JwtKey;
            var issuer = options.JwtIssuer;
            var audience = options.Audience;
            o.Authority = options.Authority;
            o.RequireHttpsMetadata = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = issuer,
                ValidAudience = audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="services"></param>
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