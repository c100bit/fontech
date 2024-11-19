using System.Net;
using FonTech.Domain.Result;
using ILogger = Serilog.ILogger;

namespace FonTech.Api.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        logger.Error(exception, exception.Message);

        var errorMessage = exception.Message;
        var response = exception switch
        {
            UnauthorizedAccessException _ => new BaseResult
            {
                ErrorMessage = errorMessage,
                ErrorCode = (int)HttpStatusCode.Unauthorized
            },
            _ => new BaseResult
            {
                ErrorMessage = errorMessage,
                ErrorCode = (int)HttpStatusCode.InternalServerError
            }
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)response.ErrorCode;
        await context.Response.WriteAsJsonAsync(response);
    }
}