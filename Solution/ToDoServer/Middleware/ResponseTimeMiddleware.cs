namespace ToDoServer.Middleware;

public class ResponseTimeMiddleware
{
    private readonly RequestDelegate _next;

    public ResponseTimeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;

        await _next(context);

        var elapsedTime = DateTime.UtcNow - startTime;

        // Optional: Log request processing time with additional details
        //var logger = context.RequestServices.GetService<ILogger<ResponseTimeMiddleware>>();
        //if (logger != null)
        //{
        //    logger.LogInformation(
        //        $"Request {context.Request.Method} {context.Request.Path} processed in {elapsedTime.TotalMilliseconds:F2}ms ({context.Response.StatusCode})");
        //}

        Console.WriteLine($"###### Request {context.Request.Method} {context.Request.Path} processed in {elapsedTime.TotalMilliseconds:F2}ms ({context.Response.StatusCode}) %%%%%%");
    }
}




