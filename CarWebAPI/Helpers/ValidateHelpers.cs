namespace CarWebAPI.Helpers;
using System.Text.Json;
class ValidateHelpers
{

    public static bool CheckRequired(string fieldName, JsonDocument jsonObject)
    {
        if (!jsonObject.RootElement.TryGetProperty(fieldName, out JsonElement fieldValue))
        {
            return false;
        }
        return true;
    }
    public static bool CheckYear(string year)
    {
        if (!int.TryParse(year, out int yearInt) || yearInt < 1886 || yearInt > DateTime.Now.Year)
        {
            return false;
        }
        return true;

    }
    public static bool CheckMaintanenceTime(string lastMaintanenceTime)
    {
        if (!DateTime.TryParse(lastMaintanenceTime, out DateTime lastMaintainanceDateTime) || lastMaintainanceDateTime.Year < 1886 || lastMaintainanceDateTime > DateTime.Now)
        {
            return false;
        }
        return true;
    }
    public static bool CheckCarType(string carType)
    {
        if (carType != "Fuel" && carType != "Electric")
        {
            return false;
        }
        return true;
    }
    public async static Task ReturnError(HttpContext context, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        var responseData = new { message };
        var jsonResponse = JsonSerializer.Serialize(responseData);
        await context.Response.WriteAsync(jsonResponse);
        return;
    }
    public static bool CheckId(JsonDocument jsonObject, out int idInt)
    {
        if (!jsonObject.RootElement.TryGetProperty("id", out JsonElement idValue))
        {
            idInt = -1;
            return false;
        }
        if(!int.TryParse(idValue.ToString(), out idInt))
        {
            return false;
        }
        return true;
    }
}
