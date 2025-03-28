using CarWebAPI.DbContext;

namespace CarWebAPI.CarManagement
{
    public interface ICarManager
    {
        void AddCar(Car car);
    }

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


    }
}