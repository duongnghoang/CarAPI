using CarWebAPI.DbContext;

namespace CarWebAPI.CarManagement
{
    public class CarManager
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