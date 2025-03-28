using CarWebAPI.Models;

namespace CarWebAPI.DbContext;

public class InMemoryDbContext : IInMemoryDbContext
{
    private readonly List<Car> _entities = new();

    public void Add(Car entity)
    {
        _entities.Add(entity);
    }

    public IEnumerable<Car> GetAll()
    {
        return _entities;
    }

    public Car? GetById(int id)
    {
        return _entities.FirstOrDefault(e => e.Id == id) ?? null;
    }

    public Car? Update(int id, DateTime newMaintenanceDate)
    {
        var existingCar = _entities.FirstOrDefault(e => e.Id == id);
        if (existingCar != null) {
            existingCar.LastMaintenanceDate = newMaintenanceDate;
            existingCar.NextMaintenanceDate = newMaintenanceDate.AddMonths(6);
            return existingCar;
        }
        return null;
    }
}