using System.Xml;
using UnterDriver.data_classes;

namespace UnterDriver.management;

public class DriverMap
{
    private int width;
    private int height;

    private List <Driver> drivers = new();

    private Dictionary <Coordinate, Driver> driverLocation = new();

    public DriverMap (int x, int y)
    {
        width = x;
        height = y;
    }


    // ====================================
    //             MAP FUNCTIONS
    // ====================================

    public void AddDriver(Driver driver)
    {
        if (driverLocation.ContainsKey(driver.Location))
        {
            Console.WriteLine("Location is already taken by driver {0}", 
            Convert.ToString(driverLocation[driver.Location].Id));
            return;
        }
        if (driver.Location.X > width || driver.Location.Y > height)
        {
            Console.WriteLine("You entered wrong coordinates");
            return;
        }

        drivers.Add(driver);
        driverLocation[driver.Location] = driver;
    }

    public bool CheckDriverOnLocation(Coordinate location)
    {
        if (driverLocation.ContainsKey(location))
        {
            return true;
        }
        return false;
    }

    public void DisplayMap (Coordinate currentOrder)
    {
        Coordinate locationChecker = new Coordinate();
        for (int j = 0; j < height; j++)
        {
            Console.Write("|");
            for (int i = 0; i < width; i++)
            {
                locationChecker.X = i; locationChecker.Y = j;
                if (currentOrder.CoordinateEquals(locationChecker))
                {
                    Console.Write("X");
                    continue;
                }
                if (CheckDriverOnLocation(locationChecker))
                {
                    Console.Write("id{0}", driverLocation[locationChecker].Id);
                    continue;
                }
                Console.Write("-");
            }
            Console.Write("|\n");
        }
    }


    // ====================================
    //              ALGORITHMS
    // ====================================

    public List <Driver> BruteForce ()
    {
        List <Driver> drivers_Copy = new();
        List <Driver> closestDrivers = new();
        foreach (Driver driver in drivers)
        {
            drivers_Copy.Add(driver);
        }

        drivers_Copy.Sort();
        for (int i = 0; i < 5; i++)
        {
            closestDrivers.Add(drivers_Copy[i]);
        }

        return closestDrivers;
    }

    public List <Driver> OneWay ()
    {
        List <Driver> closestDrivers = new();
        PriorityQueue <Driver, int> closestQueue = new PriorityQueue<Driver, int>();
        
        foreach (Driver driver in drivers)
        {
            int distance = driver.DistanceToOrder;

            if (closestQueue.Count < 5)
            {
                closestQueue.Enqueue(driver, -distance);
            }
            else if (-distance > -closestQueue.Peek().DistanceToOrder)
            {
                closestQueue.Dequeue();
                closestQueue.Enqueue(driver, -distance);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            closestDrivers.Add(closestQueue.Peek());
            closestQueue.Dequeue();
        }
        closestDrivers.Sort();

        return closestDrivers;
    }

    public List <Driver> Radar(Coordinate currentOrder)
    {
        List <Driver> scannedDrivers = new();

        int up = currentOrder.Y - 1, down = up + 2, left = currentOrder.X - 1, right = left + 2;

        for (int radius = 1; radius <= Math.Max(
            Math.Max(width - currentOrder.X, height - currentOrder.Y),
            Math.Max(currentOrder.X, currentOrder.Y)); radius++)
        {
            if (up >= 0)
            {
                for (int horizontal = Math.Max(left, 0); horizontal < Math.Min(right + 1, width); horizontal++)
                {
                    if (driverLocation.ContainsKey(new Coordinate(horizontal, up)))
                    {
                        scannedDrivers.Add(driverLocation[new Coordinate(horizontal, up)]);
                    }
                }
            }

            if (left >= 0)
            {
                for (int vertical = Math.Max(up + 1, 0); vertical < Math.Min(down + 1, height); vertical++)
                {
                    if (driverLocation.ContainsKey(new Coordinate(left, vertical)))
                    {
                        scannedDrivers.Add(driverLocation[new Coordinate(left, vertical)]);
                    }
                }
            }

            if (down < height)
            {
                for (int horizontal = Math.Max(left + 1, 0); horizontal < Math.Min(right + 1, width); horizontal++)
                {
                    if (driverLocation.ContainsKey(new Coordinate(horizontal, down)))
                    {
                        scannedDrivers.Add(driverLocation[new Coordinate(horizontal, down)]);
                    }
                }
            }

            if (right < width)
            {
                for (int vertical = Math.Max(up + 1, 0); vertical < Math.Min(down, height); vertical++)
                {
                    if (driverLocation.ContainsKey(new Coordinate(right, vertical)))
                    {
                        scannedDrivers.Add(driverLocation[new Coordinate(right, vertical)]);
                    }
                }
            }

            if (scannedDrivers.Count >= 5) break;
            up--; left--; down++; right++;
        }

        scannedDrivers.Sort();

        List <Driver> closestDrivers = new();
        for (int i = 0; i < 5; i++)
        {
            closestDrivers.Add(scannedDrivers[i]);
        }

        return closestDrivers;
    }
}