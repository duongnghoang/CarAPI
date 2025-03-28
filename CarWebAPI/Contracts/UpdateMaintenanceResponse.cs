namespace CarWebAPI.Contracts;

public record UpdateMaintenanceResponse(int Id, string Model, DateTime NewMaintenanceDate);