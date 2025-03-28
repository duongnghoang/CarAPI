using System.Text.Json;
using CarWebAPI.Helpers;

namespace CarWebAPI.Middleware;

public class InputValidateMiddleware
{
    private readonly RequestDelegate _next;

    public InputValidateMiddleware(RequestDelegate next) 
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        context.Request.EnableBuffering();
        if (context.Request.Method == "GET" || context.Request.Method == "PUT")
        {
            await _next(context);
            return;
        }

        using var reader = new StreamReader(context.Request.Body);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; // Reset lại stream
        var jsonObject = JsonDocument.Parse(body);

        // Validate id field
        if (!ValidateHelpers.CheckId(jsonObject, out var id))
        {
            await ValidateHelpers.ReturnError(context, "Id is required.");
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
            await ValidateHelpers.ReturnError(context,
                "Invalid year value. Year must be between 1886 and current year.");
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
        if (!ValidateHelpers.CheckRequired("lastMaintenanceTime", jsonObject))
        {
            await ValidateHelpers.ReturnError(context, "lastMaintenanceTime is required.");
            return;
        }

        // Validate lastMaintanenceTime field range
        if (!ValidateHelpers.CheckMaintanenceTime(jsonObject.RootElement.GetProperty("lastMaintenanceTime")
                .ToString()))
        {
            await ValidateHelpers.ReturnError(context,
                "Invalid lastMaintenanceTime value. lastMaintenanceTime must be between 1886 and current year.");
            return;
        }


        await _next(context);
    }
}