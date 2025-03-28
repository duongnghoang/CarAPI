using CarWebAPI.CarManagement;

namespace CarWebAPI.Interfaces
{
    public interface ICarManager
    {
        IEnumerable<Car> GetAllCars();
        Car GetCarById(int id);
        void AddCar(Car car);
        void UpdateCar(int id, Car car);
        void DeleteCar(int id);
        Car GetById(int id);
    }
}