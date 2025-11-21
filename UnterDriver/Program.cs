namespace UnterDriver;

using UnterDriver.data_classes;
using UnterDriver.management;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Enter the parameters of the map \nX = ");
        int x = Convert.ToInt32(Console.ReadLine());
        Console.Write("Y = ");
        int y = Convert.ToInt32(Console.ReadLine());

        Random random = new Random();
        Coordinate order = new Coordinate(random.Next(0, x), random.Next(0, y));
        DriverMap service = new DriverMap(x, y, order);
        Console.WriteLine("The order has coordinates: {0} ; {1}", order.X, order.Y);

        Console.Write("Enter the number of drivers: ");
        int numberOfDrivers = Convert.ToInt32(Console.ReadLine());
        int temp = numberOfDrivers;
        while (temp > 0)
        {
            Coordinate randomLocation = new Coordinate(random.Next(0, x), random.Next(0, y));

            if (!randomLocation.CoordinateEquals(order) && !service.CheckDriverOnLocation(randomLocation))
            {
                service.AddDriver(new Driver(numberOfDrivers - temp, randomLocation, order));
                temp--;
            }
        }

        Console.WriteLine("___________________\n Brute Force algorithm:");
        List<Driver> closestDrivers1 = service.BruteForce();
        foreach (Driver driver in closestDrivers1)
        {
            Console.WriteLine("{0}: {1};{2} | DISTANCE = {3}", driver.Id, driver.Location.X, driver.Location.Y, Math.Sqrt(driver.Location.Delta(order)));
        }

        Console.WriteLine("___________________\n One Way algorithm:");
        List<Driver> closestDrivers2 = service.OneWay();
        foreach (Driver driver in closestDrivers2)
        {
            Console.WriteLine("{0}: {1};{2} | DISTANCE = {3}", driver.Id, driver.Location.X, driver.Location.Y, Math.Sqrt(driver.Location.Delta(order)));
        }

        Console.WriteLine("___________________\n Radar algorithm:");
        List<Driver> closestDrivers3 = service.Radar();
        foreach (Driver driver in closestDrivers3)
        {
            Console.WriteLine("{0}: {1};{2} | DISTANCE = {3}", driver.Id, driver.Location.X, driver.Location.Y, Math.Sqrt(driver.Location.Delta(order)));
        }
    }
}
