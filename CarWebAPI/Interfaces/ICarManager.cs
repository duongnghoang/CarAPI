using CarWebAPI.CarManagement;

namespace CarWebAPI.Interfaces
{
    public interface ICarManager
    {
        IEnumerable<Car> GetAllCars();
        Car GetCarById(int id);
        void AddCar(Car car);
        Car GetById(int id);
    }
}