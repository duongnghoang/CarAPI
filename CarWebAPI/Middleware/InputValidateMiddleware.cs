
using System.Text.Json;
using CarWebAPI.Helpers;
using ICarManager = CarWebAPI.Interfaces.ICarManager;
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
        public async Task InvokeAsync(HttpContext context, ICarManager carManager)
        {
            try
            {
                context.Request.EnableBuffering();
                if (context.Request.Method == "GET")
                {
                    await _next(context);
                    return;
                }
                using var reader = new StreamReader(context.Request.Body);
                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // Reset lại stream
                var jsonObject = JsonDocument.Parse(body);

                // Validate id field
                if (!ValidateHelpers.CheckId(jsonObject, out int id))
                {
                    await ValidateHelpers.ReturnError(context, "Id is required.");
                    return;
                }
                if (carManager.GetById(id) != null)
                {
                    await ValidateHelpers.ReturnError(context, "Id already exists.");
                    return;
                }

                // Validate make field
                if (!ValidateHelpers.CheckRequired("make", jsonObject))
                {
                    await ValidateHelpers.ReturnError(context, "Make is required.");
                    return;
                }
                // Validate model field
                if (!ValidateHelpers.CheckRequired("model", jsonObject))
                {
                    await ValidateHelpers.ReturnError(context, "Model is required.");
                    return;
                }
                // Validate year field
                if (!ValidateHelpers.CheckRequired("year", jsonObject))
                {
                    await ValidateHelpers.ReturnError(context, "Year is required.");
                    return;
                }
                // Validate year field range
                if (!ValidateHelpers.CheckYear(jsonObject.RootElement.GetProperty("year").ToString()))
                {
                    await ValidateHelpers.ReturnError(context, "Invalid year value. Year must be between 1886 and current year.");
                    return;
                }
                // Validate carType field
                if (!ValidateHelpers.CheckRequired("carType", jsonObject))
                {
                    await ValidateHelpers.ReturnError(context, "CarType is required.");
                    return;
                }
                // Validate carType field value
                if (!ValidateHelpers.CheckCarType(jsonObject.RootElement.GetProperty("carType").ToString()))
                {
                    await ValidateHelpers.ReturnError(context, "Invalid carType value. CarType must be Fuel or Electric.");
                    return;
                }
                // Validate lastMaintanenceTime field
                if (!ValidateHelpers.CheckRequired("lastMaintanenceTime", jsonObject))
                {
                    await ValidateHelpers.ReturnError(context, "lastMaintanenceTime is required.");
                    return;
                }
                // Validate lastMaintanenceTime field range
                if (!ValidateHelpers.CheckMaintanenceTime(jsonObject.RootElement.GetProperty("lastMaintanenceTime").ToString()))
                {
                    await ValidateHelpers.ReturnError(context, "Invalid lastMaintanenceTime value. lastMaintanenceTime must be between 1886 and current year.");
                    return;
                }


                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error: " + ex.Message);
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
