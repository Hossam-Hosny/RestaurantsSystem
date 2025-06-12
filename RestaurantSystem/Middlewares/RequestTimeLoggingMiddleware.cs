
using System.Diagnostics;

namespace Restaurant.API.Middlewares;

public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
       var stopWatch= Stopwatch.StartNew();

        await next.Invoke(context);

        stopWatch.Stop();


        if (stopWatch.ElapsedMilliseconds /1000 > 4)
        {
            _logger.LogInformation("request [{verb} at {path} took {time} ms]"
                , context.Request.Method, context.Request.Path, stopWatch.ElapsedMilliseconds);
        }

    }
}
