namespace CarWebAPI.Contracts;

public record AddCarRequest(int Id, string Make, string Model, int Year, string CarType, DateTime LastMaintanenceTime);