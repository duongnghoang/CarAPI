using System.Text;

namespace CarWebAPI.Middleware;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var request = context.Request;

        request.EnableBuffering();

        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            var body = await reader.ReadToEndAsync();
            request.Body.Position = 0;

            var log = $"Schema: {request.Scheme}\n" +
                      $"Host: {request.Host}\n" +
                      $"Path: {request.Path}\n" +
                      $"QueryString: {request.QueryString}\n" +
                      $"Request Body: {body}\n\n";

            await File.AppendAllTextAsync("logs.txt", log);
        }

        await _next(context);
    }
}