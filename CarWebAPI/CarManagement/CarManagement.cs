namespace NashTechAssignment
{
    public class CarManagement
    {
        public List<Car> carList;

        public CarManagement()
        {
            carList = new List<Car>();
        }

        public void AddCar(Car car)
        {
            carList.Add(car);
        }

        public void ViewAllCars()
        {
            DisplayCarList(carList);
        }

        public List<Car> SearchCarByMake(string make)
        {
            return carList.Where(car => car.make.Equals(make, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Car> FilterCarsByType(CarType carType)
        {
            switch (carType)
            {
                case CarType.Fuel:
                    return carList.Where(car => car is IFuelable).ToList();
                case CarType.Electric:
                    return carList.Where(car => car is IChargable).ToList();
                default:
                    return null;
            }
        }

        public string RemoveCarByModel(string model)
        {
            var carToRemove = carList.FirstOrDefault(car => car.model.Equals(model, StringComparison.OrdinalIgnoreCase));
            if (carToRemove != null)
            {
                carList.Remove(carToRemove);
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