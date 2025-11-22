using UnterDriver.data_classes;
using UnterDriver.management;

namespace UnterDriverNUnitTests;

public class EdgeCaseTests
{
    // ====================================
    //            EXACTLY 5 DRIVERS
    // ====================================

    [Test]
    public void AllAlgorithms_Exactly5Drivers_ReturnAllDrivers()
    {
        Coordinate order = new Coordinate(5, 5);
        DriverMap driverMap = new DriverMap(10, 10);
        
        for (int i = 0; i < 5; i++)
        {
            driverMap.AddDriver(new Driver(i, new Coordinate(i, i), order));
        }

        List <Driver> bruteForceResult = driverMap.BruteForce();
        List <Driver> oneWayResult = driverMap.OneWay();
        List <Driver> radarResult = driverMap.Radar(order);

        Assert.AreEqual(5, bruteForceResult.Count);
        Assert.AreEqual(5, oneWayResult.Count);
        Assert.AreEqual(5, radarResult.Count);
    }


    // ====================================
    //      DRIVERS AT EQUAL DISTANCES
    // ====================================

    [Test]
    public void BruteForce_DriversAtEqualDistances_ReturnsCorrectClosestDrivers()
    {
        Coordinate order = new Coordinate(5, 5);
        DriverMap driverMap = new DriverMap(10, 10);
        
        driverMap.AddDriver(new Driver(1, new Coordinate(5, 6), order));
        driverMap.AddDriver(new Driver(2, new Coordinate(5, 4), order));
        driverMap.AddDriver(new Driver(3, new Coordinate(6, 5), order));
        driverMap.AddDriver(new Driver(4, new Coordinate(4, 5), order));
        driverMap.AddDriver(new Driver(5, new Coordinate(6, 6), order));
        driverMap.AddDriver(new Driver(6, new Coordinate(4, 4), order));
        driverMap.AddDriver(new Driver(7, new Coordinate(6, 4), order));
        driverMap.AddDriver(new Driver(8, new Coordinate(4, 6), order));

        List <Driver> bruteForceResult = driverMap.BruteForce();
        
        foreach (Driver driver in bruteForceResult)
        {
            Assert.LessOrEqual(driver.DistanceToOrder, 2);
        }
    }


    // ====================================
    //         MAP BOUNDARIES TEST
    // ====================================

    [Test]
    public void Radar_DriversAtMapEdges_ReturnsCorrectClosestDrivers()
    {
        Coordinate order = new Coordinate(0, 0);
        DriverMap driverMap = new DriverMap(10, 10);
        
        driverMap.AddDriver(new Driver(1, new Coordinate(0, 1), order));
        driverMap.AddDriver(new Driver(2, new Coordinate(1, 0), order));
        driverMap.AddDriver(new Driver(3, new Coordinate(1, 1), order));
        driverMap.AddDriver(new Driver(4, new Coordinate(0, 2), order));
        driverMap.AddDriver(new Driver(5, new Coordinate(2, 0), order));
        driverMap.AddDriver(new Driver(6, new Coordinate(9, 9), order));

        List<Driver> bruteForceResult = driverMap.BruteForce();
        List<Driver> radarResult = driverMap.Radar(order);

        List <int> bruteForceDistances = bruteForceResult.Select(d => d.DistanceToOrder).ToList();
        List <int> radarDistances = radarResult.Select(d => d.DistanceToOrder).ToList();

        CollectionAssert.AreEqual(bruteForceDistances, radarDistances);
    }


    // ====================================
    //          LARGE DATASET TEST
    // ====================================

    [Test]
    public void AllAlgorithms_With1000Drivers_ReturnCorrectClosestDrivers()
    {
        Coordinate order = new Coordinate(50, 50);
        DriverMap driverMap = new DriverMap(100, 100);
        
        Random random = new Random(12345);
        int driversAdded = 0;
        
        while (driversAdded < 1000)
        {
            Coordinate randomLocation = new Coordinate(random.Next(0, 100), random.Next(0, 100));

            if (!randomLocation.CoordinateEquals(order) && !driverMap.CheckDriverOnLocation(randomLocation))
            {
                driverMap.AddDriver(new Driver(driversAdded, randomLocation, order));
                driversAdded++;
            }
        }

        List <Driver> bruteForceResult = driverMap.BruteForce();
        List <Driver> oneWayResult = driverMap.OneWay();
        List <Driver> radarResult = driverMap.Radar(order);

        List <int> bruteForceDistances = bruteForceResult.Select(d => d.DistanceToOrder).ToList();
        List <int> oneWayDistances = oneWayResult.Select(d => d.DistanceToOrder).ToList();
        List <int> radarDistances = radarResult.Select(d => d.DistanceToOrder).ToList();

        CollectionAssert.AreEqual(bruteForceDistances, oneWayDistances);
        CollectionAssert.AreEqual(bruteForceDistances, radarDistances);
    }
}