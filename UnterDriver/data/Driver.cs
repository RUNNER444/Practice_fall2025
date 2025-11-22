namespace UnterDriver.data_classes;

public class Driver:IComparable
{
    public int Id {get;}
    public Coordinate Location {get; set;}
    public int DistanceToOrder {get; set;}
    public int CompareTo(object incomingObject)
    {
        Driver driver = incomingObject as Driver;
        return this.DistanceToOrder.CompareTo(driver.DistanceToOrder);
    }

    public Driver(int id, Coordinate location, Coordinate order)
    {
        Id = id;
        Location = location;
        DistanceToOrder = location.Delta(order);
    }
}