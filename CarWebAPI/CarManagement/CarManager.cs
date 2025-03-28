using CarWebAPI.Common;
using CarWebAPI.Interfaces;

namespace CarWebAPI.CarManagement
{
    public class CarManager
    {
        public List<Car> CarList;

        public CarManager()
        {
            CarList = new List<Car>();
        }

        public void AddCar(Car car)
        {
            CarList.Add(car);
        }

        public void ViewAllCars()
        {
            DisplayCarList(CarList);
        }

        public List<Car> SearchCarByMake(string make)
        {
            return CarList.Where(car => car.Make.Equals(make, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Car> FilterCarsByType(CarType carType)
        {
            switch (carType)
            {
                case CarType.Fuel:
                    return CarList.Where(car => car is IFuelable).ToList();
                case CarType.Electric:
                    return CarList.Where(car => car is IChargable).ToList();
                default:
                    return null;
            }
        }

        public string RemoveCarByModel(string model)
        {
            var carToRemove = CarList.FirstOrDefault(car => car.Model.Equals(model, StringComparison.OrdinalIgnoreCase));
            if (carToRemove != null)
            {
                CarList.Remove(carToRemove);
                return "Remove car successfully";
            }
            return "There is no car need to remove";
        }

        public void DisplayCarList(List<Car> carList)
        {
            Console.WriteLine("Listing all the cars");
            for (int i = 0; i < carList.Count(); i++)
            {
                Console.WriteLine($"Car No.{i}:");
                carList[i].DisplayDetails();
            }
        }
    }
}