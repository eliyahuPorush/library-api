
using Newtonsoft.Json;

namespace Api.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<GlobalExceptionMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            await HandleExceptionAsync(context, e, logger);
        }
    }
    
    private static Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
    {
        var response = new {error = "internal server error"};
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        logger.LogError(exception.Message);
        return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
    }
}