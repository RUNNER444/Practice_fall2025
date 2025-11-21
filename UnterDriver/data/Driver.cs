namespace UnterDriver.data_classes;

public class Driver:IComparable
{
    public int Id {get;}
    public Coordinate Location {get; set;}
    public Coordinate Order {get; set;}
    public int CompareTo(object incomingObject)
    {
        Driver driver = incomingObject as Driver;
        return this.Location.Delta(this.Order).CompareTo(driver.Location.Delta(this.Order));
    }

    public Driver(int id, Coordinate location, Coordinate order)
    {
        Id = id;
        Location = location;
        Order = order;
    }
}