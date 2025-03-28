
using System.Text.Json;

namespace Middleware
{
    public class InputValidateMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<InputValidateMiddleware> _logger;
        public InputValidateMiddleware(RequestDelegate next, ILogger<InputValidateMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }


        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();

                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset lại stream
                var jsonObject = JsonDocument.Parse(body);
                if (!jsonObject.RootElement.TryGetProperty("make", out JsonElement make))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Make is required.");
                    return;
                }
                if (!jsonObject.RootElement.TryGetProperty("model", out JsonElement model))
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Model is required.");
                    return;
                }
                if(!jsonObject.RootElement.TryGetProperty("year", out JsonElement year)){
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Year is required.");
                    return;
                }
                if(!int.TryParse(year.ToString(), out int yearInt) || yearInt <1886 || yearInt >DateTime.Now.Year)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid year value. Year must be between 1886 and current year.");
                    return;
                }
                if(!jsonObject.RootElement.TryGetProperty("lastMaintainanceDate", out JsonElement lastMaintainanceDate)){
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("lastMaintainanceDate is required.");
                    return;
                }
                if(!DateTime.TryParse(lastMaintainanceDate.ToString(), out DateTime lastMaintainanceDateTime) || lastMaintainanceDateTime.Year < 1886 || lastMaintainanceDateTime > DateTime.Now)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Invalid lastMaintainanceDate value. lastMaintainanceDate must be between 1886 and current year.");
                    return;
                }


                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
                return;
            }

        }
    }
    public static class InputValidateMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidateMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InputValidateMiddleware>();
        }
    }

}
