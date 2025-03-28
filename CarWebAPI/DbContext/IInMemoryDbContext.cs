using CarWebAPI.CarManagement;

namespace CarWebAPI.DbContext;

public interface IInMemoryDbContext
{
    void Add(Car entity);
    IEnumerable<Car> GetAll();
    Car GetById(int id);
}