namespace UnterDriver.data_classes;

public record struct Coordinate(int X, int Y)
{
    public int Delta(Coordinate point)
    {
        int dx = X - point.X;
        int dy = Y - point.Y;
        return (dx * dx) + (dy * dy);
    }

    public bool CoordinateEquals (Coordinate otherCoordinate)
    {
        if (X == otherCoordinate.X && Y == otherCoordinate.Y)
        {
            return true;
        }
        return false;
    }
};