using CarWebAPI.DbContext;
using CarWebAPI.Interfaces;

namespace CarWebAPI.CarManagement
{

    public class CarManager : ICarManager
    {
        private readonly IInMemoryDbContext _dbContext;

        public CarManager(IInMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCar(Car car)
        {
            _dbContext.Add(car);
        }

        public void DeleteCar(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Car> GetAllCars()
        {
            return _dbContext.GetAll();
        }

        public Car GetById(int id)
        {
            return _dbContext.GetById(id);
        }

        public Car GetCarById(int id)
        {
            return _dbContext.GetById(id);
        }
        public void UpdateCar(int id, DateTime newMaintenanceDate)
        {
            var existingCar = _dbContext.GetById(id);
            if (existingCar != null)
            {
                _dbContext.UpdateCar(id, newMaintenanceDate);
            }
        }
    }
}