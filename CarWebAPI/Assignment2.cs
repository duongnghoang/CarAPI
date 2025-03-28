//using System.Globalization;
//using CarWebAPI.CarManagement;
//using CarWebAPI.Common;
//using CarWebAPI.Interfaces;

//namespace CarWebAPI;

//internal class App
//{
//    private readonly CarManager _carManager = new();

//    public void Run()
//    {
//        while (true)
//        {
//            DisplayMenu();
//            Console.Write("> ");
//            int choice;
//            while (!int.TryParse(Console.ReadLine(), out choice))
//            {
//                Console.WriteLine("Invalid choice !! Please enter a valid choice");
//                Console.Write("> ");
//            }

//            switch (choice)
//            {
//                case 1:
//                    AddACar();
//                    break;
//                case 2:
//                    ViewAllCar();
//                    break;
//            }
//        }
//    }

//    private void RemoveCarByModel()
//    {
//        var model = GetUserModel();
//        var response = _carManager.RemoveCarByModel(model);
//        Console.WriteLine(response);
//    }

//    public void DisplayMenu()
//    {
//        Console.WriteLine("Menu: ");
//        Console.WriteLine("1. Add a Car");
//        Console.WriteLine("2. View All Cars");
//    }

//    public void AddACar()
//    {
//        string make;
//        string model;
//        int year;
//        CarType carType;
//        DateTime lastMaintanenceTime;


//        make = GetUserMake();

//        model = GetUserModel();
//        year = GetUserYear();

//        lastMaintanenceTime = GetUserLastMaintenanceDate(year);

//        carType = GetUserType();

//        switch (carType)
//        {
//            case CarType.Fuel:
//                Console.WriteLine("You have selected a Fuel car.");
//                var fuelCar = new FuelCar(make, model, year, lastMaintanenceTime);

//                fuelCar.DisplayDetails();

//                RefillCar(fuelCar);

//                _carManager.AddCar(fuelCar);

//                break;
//            case CarType.Electric:
//                Console.WriteLine("You have selected an Electric car.");
//                var electricCar = new ElectricCar(make, model, year, lastMaintanenceTime);

//                electricCar.DisplayDetails();

//                RefillCar(electricCar);

//                _carManager.AddCar(electricCar);

//                break;
//            default:
//                Console.WriteLine("Invalid car type selected.");
//                break;
//        }

//        //Car newCar = new Car(make, model, year, lastMaintanenceTime);
//        Console.WriteLine("Press Enter to Continue.");
//        Console.ReadLine();
//    }

//    public void ViewAllCar()
//    {
//        _carManager.ViewAllCars();
//        Console.WriteLine("Press Enter to Continue.");
//        Console.ReadLine();
//    }

//    public void SearchCarByMake()
//    {
//        var make = GetUserMake();
//        List<Car> makedCar = _carManager.SearchCarByMake(make);
//        if (makedCar == null)
//        {
//            Console.WriteLine("Sorry, your operation cannot be process due to lack of information");
//            Console.WriteLine("Press Enter to Continue.");
//            Console.ReadLine();
//            return;
//        }

//        Console.WriteLine($"Here is the car from {make}");
//        _carManager.DisplayCarList(makedCar);
//        Console.WriteLine("Press Enter to Continue.");
//        Console.ReadLine();
//    }

//    public void FilterCarsByType()
//    {
//        var type = GetUserType();
//        List<Car> filterCarList = _carManager.FilterCarsByType(type);

//        _carManager.DisplayCarList(filterCarList);
//        Console.WriteLine("Press Enter to Continue.");
//        Console.ReadLine();
//    }


//    public void RefillCar(Car car)
//    {
//        Console.WriteLine("Do you want to refuel/charge the car ? (Y/N): ");
//        var userInput = Console.ReadLine()?.Trim().ToUpper();
//        while (userInput != "Y" && userInput != "N")
//        {
//            Console.WriteLine("Invalid input. Please enter 'Y' for Yes or 'N' for No:");
//            Console.Write("> ");
//            userInput = Console.ReadLine()?.Trim().ToUpper();
//        }

//        if (userInput == "N") return;

//        Console.WriteLine("Enter the date and time for refuel/charge (yyyy-MM-dd HH:mm):");
//        Console.Write("> ");
//        DateTime refuelOrChargeDateTime;
//        var currentTime = DateTime.Now;
//        while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd HH:mm", null, DateTimeStyles.None,
//                   out refuelOrChargeDateTime) || refuelOrChargeDateTime < currentTime)
//        {
//            Console.WriteLine(
//                "Invalid date and time format. Please enter in the format yyyy/MM/dd HH:mm and time should be after current time:");
//            Console.Write("> ");
//        }


//        if (car is IFuelable fuelableCar)
//            fuelableCar.Refuel(refuelOrChargeDateTime);

//        else if (car is IChargable chargeableCar) chargeableCar.Charge(refuelOrChargeDateTime);
//    }


//    // Supportive function for handle user input 
//    public string GetUserMake()
//    {
//        string make;
//        Console.WriteLine("Enter car make:");
//        Console.Write("> ");
//        make = Console.ReadLine();
//        while (make.Length == 0)
//        {
//            Console.WriteLine("Car make cannot be null!!! Please try again.");
//            Console.Write("> ");
//            make = Console.ReadLine();
//        }

//        return make;
//    }

//    public string GetUserModel()
//    {
//        string model;
//        Console.WriteLine("Enter car model:");
//        Console.Write("> ");
//        model = Console.ReadLine();

//        while (model.Length == 0)
//        {
//            Console.WriteLine("Car model cannot be null!!! Please try again.");
//            Console.Write("> ");
//            model = Console.ReadLine();
//        }

//        return model;
//    }

//    public CarType GetUserType()
//    {
//        string carTypeInput;
//        Console.WriteLine("Enter car type (Fuel/Electric)");
//        Console.Write("> ");
//        carTypeInput = Console.ReadLine();

//        CarType carType;
//        int invalid;

//        while (!Enum.TryParse(carTypeInput, out carType) || int.TryParse(carTypeInput, out invalid))
//        {
//            Console.WriteLine("Invalid car type. Please enter 'Fuel' or 'Electric':");
//            Console.Write("> ");
//            carTypeInput = Console.ReadLine();
//        }

//        return carType;
//    }

//    public int GetUserYear()
//    {
//        int year;

//        Console.WriteLine("Enter car year:");
//        Console.Write("> ");
//        var currentYear = DateTime.Now.Year;
//        while (!int.TryParse(Console.ReadLine(), out year) || year < 1886 || year > currentYear)
//        {
//            Console.WriteLine("Invalid year. Please enter a valid year between 1886 and the current year:");
//            Console.Write("> ");
//        }

//        return year;
//    }

//    public DateTime GetUserLastMaintenanceDate(int carYear)
//    {
//        DateTime lastMaintenanceDate;
//        Console.WriteLine("Enter last maintenance date (yyyy-MM-dd):");

//        Console.Write("> ");
//        while (!DateTime.TryParseExact(Console.ReadLine(), "yyyy-MM-dd", null, DateTimeStyles.None,
//                   out lastMaintenanceDate) || lastMaintenanceDate.Year < carYear)
//        {
//            Console.WriteLine(
//                "Invalid date or the date is before the car's year. Please enter a valid date (yyyy-MM-dd) that is equal to or after the car's year:");
//            Console.Write("> ");
//        }

//        return lastMaintenanceDate;
//    }
//}