using FonTech.Api;
using FonTech.Application.DependencyInjection;
using FonTech.DAL.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddSwagger();
builder.Services.AddDataAccessLayer(builder.Configuration);
builder.Services.AddApplication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "FonTech.Api v1.0");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "FonTech.Api v2.0");
        c.RoutePrefix = string.Empty;
    });
    app.MapOpenApi();
}

app.MapControllers();
app.UseHttpsRedirection();
app.Run();