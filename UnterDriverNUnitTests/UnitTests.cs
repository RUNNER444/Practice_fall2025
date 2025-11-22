using UnterDriver.data_classes;
using UnterDriver.management;

namespace UnterDriverNUnitTests;

public class DriverMapTests
{
    private DriverMap driverMap;
    private Coordinate order;

    [SetUp]
    public void Setup()
    {
        order = new Coordinate(5, 5);
        driverMap = new DriverMap(10, 10);
    }


    // ====================================
    //              SETUP HELPER
    // ====================================
    private void AddTestDrivers()
    {
        driverMap.AddDriver(new Driver(0, new Coordinate(5, 6), order));
        driverMap.AddDriver(new Driver(1, new Coordinate(6, 5), order));
        driverMap.AddDriver(new Driver(2, new Coordinate(4, 5), order));
        driverMap.AddDriver(new Driver(3, new Coordinate(5, 7), order));
        driverMap.AddDriver(new Driver(4, new Coordinate(3, 5), order));
        driverMap.AddDriver(new Driver(5, new Coordinate(5, 8), order));
        driverMap.AddDriver(new Driver(6, new Coordinate(8, 8), order));
        driverMap.AddDriver(new Driver(7, new Coordinate(1, 1), order));
        driverMap.AddDriver(new Driver(8, new Coordinate(9, 9), order));
        driverMap.AddDriver(new Driver(9, new Coordinate(0, 0), order));
    }


    // ====================================
    //                DRIVER
    // ====================================

    [Test]
    public void AddDriver_ValidDriver_DriverIsAdded()
    {
        Coordinate coord = new Coordinate(3, 3);
        Driver driver = new Driver(1, coord, order);

        driverMap.AddDriver(driver);

        Assert.IsTrue(driverMap.CheckDriverOnLocation(coord));
    }


    // ====================================
    //              COORDINATE
    // ====================================

    [Test]
    public void CoordinateEquals_SameCoordinates_ReturnsTrue()
    {
        Coordinate coord1 = new Coordinate(5, 5);
        Coordinate coord2 = new Coordinate(5, 5);

        Assert.IsTrue(coord1.CoordinateEquals(coord2));
    }


    // ====================================
    //           BRUTE FORCE ALG.
    // ====================================

    [Test]
    public void BruteForce_SettedUpDrivers_ReturnsCorrectClosestDrivers()
    {
        AddTestDrivers();

        List<Driver> bruteForceResult = driverMap.BruteForce();

        List <int> bruteForceIds = bruteForceResult.Select(d => d.Id).ToList();
        CollectionAssert.Contains(bruteForceIds, 0);
        CollectionAssert.Contains(bruteForceIds, 1);
        CollectionAssert.Contains(bruteForceIds, 2);
        CollectionAssert.Contains(bruteForceIds, 3);
        CollectionAssert.Contains(bruteForceIds, 4);
    }


    // ====================================
    //              ONE WAY ALG.
    // ====================================

    [Test]
    public void OneWay_SettedUpDrivers_FindsSameDriversAsBruteForce()
    {
        AddTestDrivers();

        List<Driver> bruteForceResult = driverMap.BruteForce();
        List<Driver> oneWayResult = driverMap.OneWay();
        
        List <int> bruteForceDistances = bruteForceResult.Select(d => d.DistanceToOrder).ToList();
        List <int> oneWayDistances = oneWayResult.Select(d => d.DistanceToOrder).ToList();
        
        CollectionAssert.AreEqual(bruteForceDistances, oneWayDistances);
    }


    // ====================================
    //               RADAR ALG.
    // ====================================

    [Test]
    public void Radar_SettedUpDrivers_FindsSameDriversAsBruteForce()
    {
        AddTestDrivers();

        List<Driver> bruteForceResult = driverMap.BruteForce();
        List<Driver> radarResult = driverMap.Radar(order);
        
        List <int> bruteForceDistances = bruteForceResult.Select(d => d.DistanceToOrder).ToList();
        List <int> radarDistances = radarResult.Select(d => d.DistanceToOrder).ToList();
        
        CollectionAssert.AreEqual(bruteForceDistances, radarDistances);
    }
}