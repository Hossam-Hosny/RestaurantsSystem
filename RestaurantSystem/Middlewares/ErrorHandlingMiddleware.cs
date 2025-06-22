
using Restaurants.Domain.Exceptions;

namespace Restaurant.API.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware>_logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch(NotFoundException notfound)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync(notfound.Message);

            _logger.LogWarning(notfound.Message);

        }
        catch (ForbidException)
        {
            context.Response.StatusCode = 403;
            await context.Response.WriteAsync("Access forbidden");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("Something went wrong");
        }
    }
}
